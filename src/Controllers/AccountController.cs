using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multilang.Repositories.UserRepository;
using Multilang.Repositories.ProfilePicRepository;
using Multilang.Models.Responses;
using Multilang.Models.Requests.Accounts;
using Multilang.Models.Responses.Accounts;
using Multilang.Models.Accounts;
using Multilang.Utils;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Multilang.RequestPipeline.Filters;

namespace Multilang.Controllers
{
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private IUserRepository userRepository;
        private IProfilePicRepository picRepository;
        private IAuthTokenService<JwtBody> tokenService;

        public AccountController(IUserRepository userRepository,
            IProfilePicRepository picRepository, IAuthTokenService<JwtBody> tokenService)
        {
            this.userRepository = userRepository;
            this.picRepository = picRepository;
            this.tokenService = tokenService;
        }


        /// <summary>
        /// Registers a user
        /// </summary>
        /// <returns>Authorization Token</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<JsonResult> Register([FromBody] RegistrationModel registrationModel,
            [FromServices] LangCodes langCodes)
        {
            if (userRepository.UserExists(registrationModel.displayName))
            {
                return Json(new BaseResponse(false, "Name is already taken"));
            }

            User user = new User
            {
                displayName = registrationModel.displayName,
                passwordHash = registrationModel.passHash,
                language = registrationModel.language,
                langCode = langCodes.GetCode(registrationModel.language),
                firebaseToken = registrationModel.firebaseToken
            };
            
            bool added = userRepository.AddUser(user);
            if (!added)
            {
                return Json(new BaseResponse(false, "An error occurred while adding user"));
            }

            string token = tokenService.Issue(new JwtBody 
            {  
                issuedAt = (DateTime.UtcNow.Subtract(
                    new System.DateTime(1970, 1, 1))).TotalSeconds.ToString(),
                id = user.id.ToString()
            });

            return Json(new TokenResponse 
            { 
                succeeded = true, 
                token = token,
                language = user.language 
            });
        }

        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <returns>Authorization Token</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<JsonResult> LogIn([FromBody] LoginModel loginModel)
        {
            User user = userRepository.FindUser(u => u.displayName == loginModel.displayName 
                && u.passwordHash == loginModel.passHash);
            
            if (user == null)
            {
                return Json(new BaseResponse(false, "Invalid Credentials"));
            }

            string token = tokenService.Issue(new JwtBody 
            {  
                issuedAt = (DateTime.UtcNow.Subtract(
                    new System.DateTime(1970, 1, 1))).TotalSeconds.ToString(),
                id = user.id.ToString()
            });

            return Json(new TokenResponse 
            { 
                succeeded = true, 
                token = token,
                language = user.language
            });
        }

        /// <summary>
        /// Updates user's Firebase token.
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("updateFirebaseToken")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public JsonResult Firebase([FromBody] UpdateFirebaseTokenModel tokenModel, 
            JwtBody jwtBody)
        {
            return Json(jwtBody);
        }
    }
}