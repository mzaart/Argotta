using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Multilang.Models.Db;
using Multilang.Models.Jwt;
using Multilang.Models.Requests.Chat;
using Multilang.Models.Requests.Groups;
using Multilang.Models.Responses;
using Multilang.Models.Responses.Groups;
using Multilang.Repositories;
using Multilang.RequestPipeline.Filters;

namespace Multilang.Controllers
{
    [ServiceFilter(typeof(TokenAuth))]
    [ValidateModel]
    [Route("/api/[controller]")]
    public class GroupsController : Controller
    {
        private IRepository<Group> groupRepo;

        public GroupsController(IRepository<Group> groupRepo)
        {
            this.groupRepo = groupRepo;
        }

        /// <summary>
        /// Creates a Group.
        /// </summary>
        [HttpPost("/api/Groups/")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> Create([FromBody] CreateGroupRequest req)
        {
            var group = new Group()
            {
                Title = req.Title,
                AdminId = req.AdminId,
                membersList = req.Members
            };

            await groupRepo.Insert(group);
            await groupRepo.Save();

            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Gets members of a group
        /// </summary>
        [HttpGet("members")]
        [ProducesResponseType(typeof(GetMembersResponse), 200)]
        public async Task<JsonResult> GetMembers([FromBody] GetMembersRequest req)
        {
            var group = await groupRepo.GetById(req.Title);
            if (group == null)
            {
                return Json(new BaseResponse(false, "Group does not exist"));
            }

            return Json(new GetMembersResponse()
            {
                members = group.membersList
            });
        }

        /// <summary>
        /// Adds members to a group
        /// </summary>
        [HttpPost("members")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> AddMembers([FromBody] AddMembersRequest req)
        {
            var group = await groupRepo.GetById(req.Title);
            if (group == null)
            {
                return Json(new BaseResponse(false, "Group does not exist"));
            }

            group.membersList.Concat(req.Members);
            await groupRepo.Save();

            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Removed members from a group
        /// </summary>
        [HttpDelete("members")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> RemoveMembers([FromBody] AddMembersRequest req)
        {
            var group = await groupRepo.GetById(req.Title);
            if (group == null)
            {
                return Json(new BaseResponse(false, "Group does not exist"));
            }

            var ids = req.Members.Select(m => m.Id);
            group.membersList = new HashSet<GroupMember>(
                group.membersList.Where(m => !ids.Contains(m.Id)));
            await groupRepo.Save();

            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Sends a message to a group.
        /// </summary>
        [HttpPost("sendMessage")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<ActionResult> SendMessage([FromBody] SendMessageRequest req, 
            [FromHeader] JwtBody jwt)
        {
            var group = await groupRepo.GetById(req.Title);
            if (group == null)
            {
                return Json(new BaseResponse(false, "Group does not exist"));
            }

            var sendMessageModel = new SendMessagesModel()
            {
                message = req.message,
                senderLanguage = jwt.language,
                recipientIds = group.membersList.Select(m => m.Id).ToList()
            };

            return RedirectToAction("SendMessages", "ChatController", new RouteValueDictionary
            (
                new 
                {
                    messageModal = sendMessageModel,
                    jwtBody = jwt
                }
            ));
        }

        /// <summary>
        /// Sends a binary file to a group.
        /// </summary>
        [HttpPost("sendBinary")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<ActionResult> SendBinary([FromBody] SendBinaryRequest req, 
            [FromHeader] JwtBody jwt)
        {
            var group = await groupRepo.GetById(req.Title);
            if (group == null)
            {
                return Json(new BaseResponse(false, "Group does not exist"));
            }

            var sendBinaryModel = new SendBinariesModel()
            {
                base64Data = req.base64Data,
                senderLanguage = jwt.language,
                recipientIds = group.membersList.Select(m => m.Id).ToList()
            };

            return RedirectToAction("SendBinaries", "ChatController", new RouteValueDictionary
            (
                new 
                {
                    binariesModel = sendBinaryModel,
                    jwtBody = jwt
                }
            ));
        }
    }
}