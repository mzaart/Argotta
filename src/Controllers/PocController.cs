using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Multilang.Services.MessagingServices;
using Multilang.Models.Messages;
using Multilang.Models.Requests;
using Multilang.Models.Responses;
using Multilang.Repositories.UserRepository;
using Multilang.Repositories.ProfilePicRepository;
using Multilang.Models.Requests.Accounts;

namespace Multilang.Controllers
{

    [Route("api/[controller]")]
    public class PocController : Controller
    {
        private readonly IMessagingService messagingService;
        private readonly IUserRepository userRepository;
        private readonly IProfilePicRepository picRepository;

        public PocController(IMessagingService messagingService, 
            IUserRepository userRepository, IProfilePicRepository picRepository)
        {
            this.messagingService = messagingService;
            this.userRepository = userRepository;
            this.picRepository = picRepository;
        }

        /// <summary>
        /// Translates and sends a message.
        /// </summary>
        [HttpPost("send")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> Send([FromBody] SendMessageModel sendMessageModel) 
        {
            try 
            {
                var response = await this.messagingService.SendMessage(sendMessageModel.senderId, 
                    sendMessageModel.recepientId, sendMessageModel.message);
                
                if (response.IsSuccessStatusCode) 
                {
                    return Json(new BaseResponse(true));
                }  
                else
                {
                    return Json(new BaseResponse(false, response.StatusCode.ToString()));
                }

            } 
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return Json(new BaseResponse(false, e.Message));
            }
        }

        /// <summary>
        /// Updates user's Firebase token.
        /// </summary>
        [HttpPost("updateFirebaseToken")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public JsonResult Firebase([FromBody] UpdateFirebaseTokenModel tokenModel) {
            try 
            {
               userRepository.UpdateFirebaseToken(tokenModel.id, tokenModel.token);
               return Json(new BaseResponse(true));
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return Json(new BaseResponse(false, e.Message));
            }
        }

        [HttpPost("test")]
        public async Task<IActionResult> test([FromBody] SetProfilePicModal modal)
        {
            bool r = await picRepository.setProfilePic("0", modal.picBase64);
            return Json(new BaseResponse(r));
        }
    }
}
