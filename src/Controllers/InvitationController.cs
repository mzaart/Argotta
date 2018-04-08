/*using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multilang.Repositories.UserRepository;
using Multilang.Repositories.ProfilePicRepository;
using Multilang.Models.Responses;
using Multilang.Models.Requests.Accounts;
using Multilang.Models.Responses.Accounts;
using Multilang.Models.Db;
using Multilang.Utils;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Multilang.RequestPipeline.Filters;
using Multilang.Models.Responses.Invitations;
using Multilang.Models.Requests.Invitations;
using Multilang.Repositories;
using Multilang.Services.MessagingServices;

namespace Multilang.Controllers
{
    [ValidateModel]
    [Route("/api/[controller]")]
    public class InvitationController : Controller
    {
        private IRepository<User> userRepository;
        private IAuthTokenService<JwtBody> tokenService;

        public InvitationController(IRepository<User> userRepository, 
            IAuthTokenService<JwtBody> tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// Gets a user's invitations
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpGet("invitations")]
        [ProducesResponseType(typeof(GetInvitationsResponse), 200)]
        public async Task<JsonResult> GetInvitations([FromHeader] JwtBody jwt) 
        {   
            User user = await userRepository.GetById(jwt.id);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            return Json(new GetInvitationsResponse
            {
                succeeded = true,
                invitations = user.invitations
            });
        }

        /// <summary>
        /// Invites a user to a chat
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("invitations")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> Invite([FromBody] RecipientModel model, 
            [FromServices] IMessagingService messagingService, [FromHeader] JwtBody jwt)
        {
            var users = await userRepository
                            .GetAll()
                            .Where(u => u.Id.ToString() == jwt.id || u.Id.ToString() == model.recipientId)
                            .ToListAsync();

            var sender = users.First(u => u.Id.ToString() == jwt.id);
            var recipient = users.FirstOrDefault(u => u.Id.ToString() == model.recipientId);

            if (recipient == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (recipient.blockedIds.Contains(jwt.id))
            {
                return Json(new BaseResponse(false, "Blocked by user"));
            }

            var inv = new Invitation
            {
                senderId = jwt.id,
                senderDisplayName = jwt.displayName,
            };

            recipient.invitations.Add(inv);

            await userRepository.Save();
            await messagingService.SendInvitation(inv, sender, recipient);

            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Accepsts or declines an invitation
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("Respond")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> Respond([FromBody] InvitationResponseModel invitationModel, 
            [FromHeader] JwtBody jwt)
        {
            User user = await userRepository.GetById(jwt.id);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            var invitation = user.invitations.Find(i => i.senderId == invitationModel.senderId);
            
            if (invitation == null)
            {
                return Json(new BaseResponse(false, "Invitation does not exist"));
            }

            if (invitationModel.accepted)
            {
                invitation.accepted = true;
            }
            else
            {
                user.invitations.Remove(invitation);
            }

            await userRepository.Save();
            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Blocks a user
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("block")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> Block([FromBody] RecipientModel recipient, [FromHeader] JwtBody jwt)
        {
            User user = await userRepository.GetById(jwt.id);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (user.blockedIds.Contains(recipient.recipientId))
            {
                return Json(new BaseResponse(false, "User already blocked"));
            }

            user.blockedIds.Add(recipient.recipientId);
            user.invitations.RemoveAll(i => i.senderId == recipient.recipientId);

            await userRepository.Save();
            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Unblocks a user
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpDelete("block")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> UnBlock([FromBody] RecipientModel recipient, [FromHeader] JwtBody jwt)
        {
            User user = await userRepository.GetById(jwt.id);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (!user.blockedIds.Contains(recipient.recipientId))
            {
                return Json(new BaseResponse(false, "User not blocked"));
            }

            user.blockedIds.Remove(recipient.recipientId);
            return Json(new BaseResponse(true));
        }

        
   }
}
*/