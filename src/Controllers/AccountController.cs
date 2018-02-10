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
    [ValidateModel]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private IUserRepository userRepository;
        private IProfilePicRepository picRepository;
        private IAuthTokenService<JwtBody> tokenService;
        private LangCodes langCodes;

        public AccountController(IUserRepository userRepository, LangCodes langCodes,
            IProfilePicRepository picRepository, IAuthTokenService<JwtBody> tokenService)
        {
            this.userRepository = userRepository;
            this.picRepository = picRepository;
            this.tokenService = tokenService;
            this.langCodes = langCodes;
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
        /// Updates user's account inforation
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("update")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public JsonResult UpdateAccount([FromBody] UpdateAccountModal accModal, JwtBody jwt)
        {
            User user = userRepository.GetUserById(jwt.id);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }
            else
            {
                if (accModal.displayName != null) user.displayName = accModal.displayName;
                if (accModal.passwordHash != null) user.passwordHash = accModal.passwordHash;
                if (accModal.firebaseToken != null) user.firebaseToken = accModal.firebaseToken;
                if (accModal.language != null)
                {
                    user.language = accModal.language;
                    user.langCode = langCodes.GetCode(user.language);
                }

                return Json(new BaseResponse(true));
            }
        }

        /// <summary>
        /// Sets user's profile picture
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("profilePicture")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<IActionResult> SetProfilePic([FromBody] SetProfilePicModal modal, 
            JwtBody jwt)
        {
            bool succeeded = await picRepository.setProfilePic(jwt.id, modal.picBase64);
            return Json(new BaseResponse(succeeded));
        }

        /// <summary>
        /// Get url to user's profile picture
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpGet("profilePicture")]
        [ProducesResponseType(typeof(ProfilePicResponse), 200)]
        public JsonResult GetProfilePic(JwtBody jwt)
        {
            string path = picRepository.getProfilePicPath(jwt.id);
            return Json(new ProfilePicResponse { succeeded = true, picUrl = path });
        }

        [ServiceFilter(typeof(TokenAuth))]
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public JsonResult DeleteAccount(JwtBody jwt)
        {
            bool deleted = userRepository.DeleteUser(jwt.id);
            return Json(new BaseResponse(deleted, deleted ? null : "User does not exist"));
        }
    }
}