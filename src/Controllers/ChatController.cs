using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Multilang.Services.MessagingServices;
using Multilang.Utils;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using System.Threading.Tasks;
using Multilang.RequestPipeline.Filters;
using Multilang.Repositories;
using Multilang.Models.Db;
using Multilang.Models.Responses;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Multilang.Models.Requests.Chat;
using System.Collections.Generic;

namespace Multilang.Controllers
{
    [ServiceFilter(typeof(TokenAuth))]
    public class ChatController : Controller
    {
        private IMessagingService messagingService;
        private IRepository<User> userRepository;

        public ChatController(IRepository<User> userRepository, IMessagingService messagingService)
        {
            this.userRepository = userRepository;
            this.messagingService = messagingService;
        }

        /// <summary>
        /// Tanslates and sends a message.
        /// </summary>
        [HttpPost("sendMessage")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> send([FromBody] SendMessageModel messageModal, JwtBody jwt)
        {
            // check if user exsts
            var recipient = await userRepository.GetById(messageModal.recipientId);
            if (recipient == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (!canSendToUser(jwt.id, recipient))
            {
                return Json(new BaseResponse(false, "Can not send to user"));
            }

            bool sent = await messagingService
                .SendMessage(jwt.id, jwt.langCode, recipient, messageModal.message);
            
            return Json(new BaseResponse(sent, sent ? null : "Message not sent"));
        }

        /// <summary>
        /// Sends a message to multiple users
        /// </summary>
        [HttpPost("sendMessages")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> SendMessages([FromBody] SendMessagesModel messageModal, 
            JwtBody jwt)
        {
            // filter out users
            var recipients = await userRepository
                .GetAll()
                .Where(u => messageModal.recipientIds.Contains(u.Id.ToString()))
                .Where(u => canSendToUser(jwt.id, u))
                .ToListAsync();

            bool sent = await messagingService.SendMessages(jwt.id, jwt.langCode, recipients, 
                messageModal.message);

            return Json(new BaseResponse(sent, sent ? null : "Not all users received message"));
        }

        /// <summary>
        /// Sends binary data to a user.
        /// </summary>
        [HttpPost("sendBinary")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> sendBinary([FromBody] SendBinaryModel binaryModel, 
            JwtBody jwt)
        {
            // check if user exsts
            var recipient = await userRepository.GetById(binaryModel.recipientId);
            if (recipient == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (!canSendToUser(jwt.id, recipient))
            {
                return Json(new BaseResponse(false, "Can not send to user"));
            }
            
            bool sent = await messagingService
                .SendBinary(jwt.id, jwt.langCode, recipient, binaryModel.fileName, 
                    binaryModel.base64Data);
            
            return Json(new BaseResponse(sent, sent ? null : "Message not sent"));
        }

        /// <summary>
        /// Sends binary data to multiple users
        /// </summary>
        [HttpPost("sendBinaries")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> SendBinaries([FromBody] SendBinariesModel binariesModel, 
            JwtBody jwt)
        {
            // filter out users
            var recipients = await userRepository
                .GetAll()
                .Where(u => binariesModel.recipientIds.Contains(u.Id.ToString()))
                .Where(u => canSendToUser(jwt.id, u))
                .ToListAsync();

            bool sent = await messagingService.SendBinaries(jwt.id, jwt.langCode, recipients, 
                binariesModel.fileName , binariesModel.base64Data);

            return Json(new BaseResponse(sent, sent ? null : "Not all users received message"));
        }

        /// <summary>
        /// Reports a poor translation to sender
        /// </summary>
        [HttpPost("poorTranslation")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> poorTranslation([FromBody] PoorTranslationModel translationModel, 
            JwtBody jwt)
        {
            // check if user exsts
            var recipient = await userRepository.GetById(translationModel.recipientId);
            if (recipient == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (!canSendToUser(jwt.id, recipient))
            {
                return Json(new BaseResponse(false, "Can not send to user"));
            }

            bool sent = await messagingService.NotifyPoorTranslation(jwt.displayName, jwt.language,
                recipient, translationModel.message);

            return Json(new BaseResponse(sent, sent ? null : "Not all users received message"));
        }

        private bool canSendToUser(string senderId, User recipient)
        {   
            if (recipient.blockedIds.Contains(senderId))
            {
                return false;
            }
            
            if (!recipient.invitations.Exists(i => i.senderId == senderId))
            {
                return false;
            }

            return true;
        }
    }
}