using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multilang.Models.Db;
using Multilang.Models.Jwt;
using Multilang.Models.Requests.Users;
using Multilang.Models.Responses;
using Multilang.Models.Responses.Users;
using Multilang.Repositories;
using Multilang.RequestPipeline.Filters;
using Multilang.Services.AuthTokenServices;

namespace Multilang.Controllers
{
    [ServiceFilter(typeof(TokenAuth))]
    [ValidateModel]
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private IRepository<User> userRepository;
        private IAuthTokenService<JwtBody> tokenService;

        public UsersController(IRepository<User> userRepository,
            IAuthTokenService<JwtBody> tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// Gets a list of users
        /// </summary>
        [HttpGet("/api/Users")]
        [ProducesResponseType(typeof(UsersResponse), 200)]
        public async Task<UsersResponse> GetUsers([FromHeader] JwtBody jwtBody)
        {
            return new UsersResponse
            {
                Users = userRepository
                    .GetAll()
                    .Select(u => new UserResponse
                    {
                        Id = u.Id,
                        langCode = u.langCode,
                        language = u.language,
                        displayName = u.displayName,
                        fullName = u.fullName,
                        email = u.email,
                        firebaseToken = u.firebaseToken
                    })
                    .ToList()
            };
        }

        [HttpGet("getUser")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public async Task<JsonResult> GetUser([FromQuery] int id)
        {
            var user = await userRepository.GetById(id.ToString());
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exixst"));
            }
            else
            {
                return Json(new UserResponse
                {
                    Id = user.Id,
                    langCode = user.langCode,
                    language = user.language,
                    displayName = user.displayName,
                    fullName = user.fullName,
                    email = user.email,
                    firebaseToken = user.firebaseToken
                });
            }
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