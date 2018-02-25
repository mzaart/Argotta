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
    [ValidateModel]
    [Route("/api/[controller]")]
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
        public async Task<JsonResult> send([FromBody] SendMessageModel messageModel, 
            [FromHeader] JwtBody jwtBody)
        {
            // check if user exsts
            var pair = await GetUserPair(jwtBody.id, messageModel.recipientId);
            if (pair.Recipient == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (!CanSendToUser(jwtBody.id, pair.Recipient))
            {
                return Json(new BaseResponse(false, "Can not send to user"));
            }

            bool sent = await messagingService
                .SendMessage(pair.Sender, pair.Recipient, messageModel.message);
            
            return Json(new BaseResponse(sent, sent ? null : "Message not sent"));
        }

        /// <summary>
        /// Sends a message to multiple users
        /// </summary>
        [HttpPost("sendMessages")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> SendMessages([FromBody] SendMessagesModel messageModal, 
            [FromHeader] JwtBody jwtBody)
        {
            // filter out users
            var recipients = await userRepository
                .GetAll()
                .Where(u => messageModal.recipientIds.Contains(u.Id.ToString()))
                .Where(u => CanSendToUser(jwtBody.id, u))
                .ToListAsync();

            var sender = await userRepository.GetById(jwtBody.id);
            bool sent = await messagingService.SendMessages(sender, recipients, 
                messageModal.message);

            return Json(new BaseResponse(sent, sent ? null : "Not all users received message"));
        }

        /// <summary>
        /// Sends binary data to a user.
        /// </summary>
        [HttpPost("sendBinary")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> sendBinary([FromBody] SendBinaryModel binaryModel, 
            [FromHeader] JwtBody jwtBody)
        {
            // check if user exsts
            var pair = await GetUserPair(jwtBody.id, binaryModel.recipientId);
            if (pair.Recipient == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (!CanSendToUser(jwtBody.id, pair.Recipient))
            {
                return Json(new BaseResponse(false, "Can not send to user"));
            }
            
            bool sent = await messagingService
                .SendBinary(pair.Sender, pair.Recipient, binaryModel.fileName, 
                    binaryModel.base64Data);
            
            return Json(new BaseResponse(sent, sent ? null : "Message not sent"));
        }

        /// <summary>
        /// Sends binary data to multiple users
        /// </summary>
        [HttpPost("sendBinaries")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> SendBinaries([FromBody] SendBinariesModel binariesModel, 
            [FromHeader] JwtBody jwtBody)
        {
            // filter out users
            var recipients = await userRepository
                .GetAll()
                .Where(u => binariesModel.recipientIds.Contains(u.Id.ToString()))
                .Where(u => CanSendToUser(jwtBody.id, u))
                .ToListAsync();

            var sender = await userRepository.GetById(jwtBody.id);
            bool sent = await messagingService.SendBinaries(sender, recipients, 
                binariesModel.fileName , binariesModel.base64Data);

            return Json(new BaseResponse(sent, sent ? null : "Not all users received message"));
        }

        /// <summary>
        /// Reports a poor translation to sender
        /// </summary>
        [HttpPost("poorTranslation")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        public async Task<JsonResult> poorTranslation([FromBody] PoorTranslationModel translationModel, 
            [FromHeader] JwtBody jwtBody)
        {
            // check if user exsts
            var pair = await GetUserPair(jwtBody.id, translationModel.recipientId);
            if (pair.Recipient == null)
            {
                return Json(new BaseResponse(false, "User does not exist"));
            }

            if (!CanSendToUser(jwtBody.id, pair.Recipient))
            {
                return Json(new BaseResponse(false, "Can not send to user"));
            }

            bool sent = await messagingService.NotifyPoorTranslation(pair.Sender,
                pair.Recipient, translationModel.message);

            return Json(new BaseResponse(sent, sent ? null : "Not all users received message"));
        }

        // returns (sender, recepient)
        private async Task<(User Sender, User Recipient)> GetUserPair(string senderId, 
            string recipientId) 
        {
            var users = await userRepository
                            .GetAll()
                            .Where(u => u.Id.ToString() == senderId 
                                || u.Id.ToString() == recipientId)
                            .ToListAsync();

            Console.WriteLine(senderId);
            var sender = users.First(u => u.Id.ToString() == senderId);
            var recipient = users.FirstOrDefault(u => u.Id.ToString() == recipientId);
              
            //return new User[] {sender, recipient};
            return (Sender: sender, Recipient: recipient);
        }

        private bool CanSendToUser(string senderId, User recipient)
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