using System.Threading.Tasks;
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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Multilang.RequestPipeline.Filters;
using Multilang.Models.Responses.Invitations;
using Multilang.Models.Requests.Invitations;

namespace Multilang.Controllers
{
    [ValidateModel]
    [Route("/api/[controller]")]
    public class InvitationController : Controller
    {
        private IUserRepository userRepository;
        private IAuthTokenService<JwtBody> tokenService;

        public InvitationController(IUserRepository userRepository, 
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
        public JsonResult GetInvitations(JwtBody jwt) 
        {   
            User user = userRepository.GetUserById(jwt.id);
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
        public JsonResult Invite([FromBody] RecipientModel recipient, JwtBody jwt)
        {
            User user = userRepository.GetUserById(recipient.recipientId);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (user.blockedIds.Contains(jwt.id))
            {
                return Json(new BaseResponse(false, "Blocked by user"));
            }

            user.invitations.Add(new Invitation
            {
                senderId = jwt.id,
                senderDisplayName = jwt.displayName,
            });

            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Accepsts or declines an invitation
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("Respond")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public JsonResult Respond([FromBody] InvitationResponseModel invitationModel, JwtBody jwt)
        {
            User user = userRepository.GetUserById(jwt.id);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            var invitation = user.invitations.Find(i => i.Id == invitationModel.invitationId);
            
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

            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Blocks a user
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("block")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public JsonResult Block([FromBody] RecipientModel recipient, JwtBody jwt)
        {
            User user = userRepository.GetUserById(jwt.id);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (user.blockedIds.Contains(recipient.recipientId))
            {
                return Json(new BaseResponse(false, "User already blocked"));
            }

            user.blockedIds.Add(recipient.recipientId);
            return Json(new BaseResponse(true));
        }

        /// <summary>
        /// Unblocks a user
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpDelete("block")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public JsonResult UnBlock([FromBody] RecipientModel recipient, JwtBody jwt)
        {
            User user = userRepository.GetUserById(jwt.id);
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
