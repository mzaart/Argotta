using System;
using System.Net.Http;
using System.Threading.Tasks;
using Multilang.Models;
using Multilang.Services.MessagingServices.Firebase;
using Multilang.Models.FirebasePayloads;
using Multilang.Services.TranslationServices;
using Multilang.Models.Messages;
using System.Linq;
using System.Collections.Generic;
using Multilang.Models.Db;
using Multilang.Repositories;

namespace Multilang.Services.MessagingServices {

    public class MessagingService : IMessagingService
    {
        private readonly FirebaseClient client;
        private readonly ITranslationService translationService;

        public MessagingService(FirebaseClient client, ITranslationService translationService) {
            this.client = client;
            this.translationService = translationService;
        }

        public async Task<Boolean> SendBinary(string senderId, string senderLangCode, User recipient, 
            string fileName, string base64Data)
        {
            var payload = new BinaryPayload(senderId, base64Data, fileName);

            var response = await client.Notify(new FcmMessage
            {
                token = recipient.firebaseToken,
                data = payload
            });

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendBinaries(string senderId, string senderLangCode, 
            List<User> recipients, string filename, string base64Data)
        {
            var payload = new BinaryPayload(senderId, base64Data, filename);
            var response = await client.Notify(new FcmMessage
            {
                registrationIds = recipients.Select(u => u.firebaseToken).ToList(),
                data = payload
            });

            return response.IsSuccessStatusCode;
        }

        public async Task<Boolean> SendMessage(string senderId, string senderLangCode,
            User recipient, string content)
        {            
            var translatedText = await translationService.Translate(content, recipient.langCode, 
                senderLangCode);

            var msg = new MessagePayload(new Message 
            {
                senderId = senderId,
                senderLangCode = senderLangCode,
                recepientId = recipient.Id.ToString(),
                recepientLang = recipient.language,
                time = (long) (DateTime.UtcNow.Subtract(
                    new System.DateTime(1970, 1, 1))).TotalSeconds,
                content = translatedText
            });

            var response = await client.Notify(new FcmMessage
            {
                token = recipient.firebaseToken,
                notification =  new FcmNotification { title = translatedText },
                data = msg
            });

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendMessages(string senderId, string senderLangCode, 
            List<User> recipients, string message)
        {
            // group by languages: <lang, users of the same lang>
            Dictionary<string, List<User>> usersByLang = recipients
                .GroupBy(u => u.langCode, u => u)
                .ToDictionary(g => g.Key, g => g.ToList());

            // translated texts: <lang, translated message in lang>
            var tasks = new List<Task<HttpResponseMessage>>();
            foreach (var langCode in usersByLang.Keys)
            {
                var translatedText =  await translationService.Translate(message, langCode, 
                senderLangCode);

                var msg = new MessagePayload(new Message 
                {
                    senderId = senderId,
                    senderLangCode = senderLangCode,
                    time = (long) (DateTime.UtcNow.Subtract(
                        new System.DateTime(1970, 1, 1))).TotalSeconds,
                    content = translatedText
                }); 

                var response = client.Notify(new FcmMessage
                {
                    registrationIds = usersByLang[langCode].Select(u => u.Id.ToString()).ToList(),
                    notification =  new FcmNotification { title = translatedText },
                    data = msg
                });

                tasks.Add(response);
            }

            return (await Task.WhenAll(tasks))
                .Select(response => response.IsSuccessStatusCode)
                .Aggregate((b1, b2) => b1 && b2);
        }

        public async Task<bool> NotifyPoorTranslation(string senderDislayName, string senderLanguage, 
            User recipient, string message)
        {
            var payload = new PoorTrasnlationPayload(senderDislayName, senderLanguage, message);
            var response = await client.Notify(new FcmMessage
            {
                token = recipient.firebaseToken,
                notification = new FcmNotification { title = "Translation not understood" },
                data = payload
            });

            return response.IsSuccessStatusCode;
        }
    }
}