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
using Multilang.Repositories;

namespace Multilang.Controllers
{
    [ValidateModel]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private IRepository<User> userRepository;
        private IProfilePicRepository picRepository;
        private IAuthTokenService<JwtBody> tokenService;
        private LangCodes langCodes;

        public AccountController(IRepository<User> userRepository, LangCodes langCodes,
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
            Console.WriteLine(registrationModel == null);
            User user =  await userRepository
                .Find(u => u.displayName == registrationModel.displayName);
                
            if (user != null)
            {
                return Json(new BaseResponse(false, "Name is already taken"));
            }

            user = new User
            {
                displayName = registrationModel.displayName,
                passwordHash = registrationModel.passHash,
                language = "English",
                langCode = "en",
                firebaseToken = registrationModel.firebaseToken
            };
            
            await userRepository.Insert(user);

            string token = tokenService.Issue(new JwtBody 
            {  
                issuedAt = (DateTime.UtcNow.Subtract(
                    new System.DateTime(1970, 1, 1))).TotalSeconds.ToString(),
                id = user.Id.ToString(),
                displayName = user.displayName,
                langCode = user.langCode
            });

            return Json(new TokenResponse 
            { 
                succeeded = true, 
                token = token,
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
            User user = await userRepository.Find(u =>
                u.displayName.ToLower() == loginModel.displayName .ToLower()
                && u.passwordHash.ToLower() == loginModel.passHash.ToLower());
            
            if (user == null)
            {
                return Json(new BaseResponse(false, "Invalid Credentials"));
            }

            Console.WriteLine("ID: " + user.Id);

            string token = tokenService.Issue(new JwtBody 
            {  
                issuedAt = (DateTime.UtcNow.Subtract(
                    new System.DateTime(1970, 1, 1))).TotalSeconds.ToString(),
                id = user.Id.ToString(),
                displayName = user.displayName,
            });

            return Json(new TokenResponse 
            { 
                succeeded = true, 
                token = token,
            });
        }

        /// <summary>
        /// Updates user's account inforation
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("update")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<JsonResult> UpdateAccount([FromBody] UpdateAccountModal accModal,
            [FromHeader] JwtBody jwt)
        {
            User user = await userRepository.GetById(jwt.id);
            if (user == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }
            else
            {
                if (accModal.displayName != null) user.displayName = accModal.displayName;
                if (accModal.passwordHash != null) user.passwordHash = accModal.passwordHash;
                if (accModal.firebaseToken != null) user.firebaseToken = accModal.firebaseToken;
                if (accModal.fullName != null) user.fullName = accModal.fullName;
                if (accModal.email != null) user.email = accModal.email;
                if (accModal.language != null)
                {
                    user.language = accModal.language;
                    user.langCode = langCodes.GetCode(user.language);
                }

                await userRepository.Save();

                string token = tokenService.Issue(new JwtBody 
                {  
                    issuedAt = (DateTime.UtcNow.Subtract(
                        new System.DateTime(1970, 1, 1))).TotalSeconds.ToString(),
                    id = user.Id.ToString(),
                    displayName = user.displayName,
                    langCode = user.langCode
                });

                return Json(new TokenResponse { succeeded = true, token = token });
            }
        }

        /// <summary>
        /// Sets user's profile picture
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpPost("profilePicture")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<IActionResult> SetProfilePic([FromBody] SetProfilePicModal modal, 
            [FromHeader] JwtBody jwt)
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
        public JsonResult GetProfilePic([FromHeader] JwtBody jwt)
        {
            string path = picRepository.getProfilePicPath(jwt.id);
            return Json(new ProfilePicResponse { succeeded = true, picUrl = path });
        }

        /// <summary>
        /// Deletes user's account
        /// </summary>
        [ServiceFilter(typeof(TokenAuth))]
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> DeleteAccount([FromHeader] JwtBody jwt)
        {
            User u = await userRepository.GetById(jwt.id);
            if (u == null) {
                Json(new BaseResponse(false, "User does not exist"));
            }
            
            await userRepository.Delete(u);
            return Json(new BaseResponse(true));
        }
    }
}