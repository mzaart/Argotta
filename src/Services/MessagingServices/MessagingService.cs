using System;
using System.Net.Http;
using System.Threading.Tasks;
using Multilang.Models;
using Multilang.Repositories.UserRepository;
using Multilang.Services.MessagingServices.Firebase;
using Multilang.Services.MessagingServices.Payloads;
using Multilang.Services.TranslationServices;
using Multilang.Models.Messages;

namespace Multilang.Services.MessagingServices {

    public class MessagingService : IMessagingService
    {
        private readonly IUserRepository userRepository;
        private readonly FirebaseClient client;
        private readonly ITranslationService translationService;

        public MessagingService(IUserRepository userRepository, FirebaseClient client,
            ITranslationService translationService) {
            this.userRepository = userRepository;
            this.client = client;
            this.translationService = translationService;
        }

        async Task<HttpResponseMessage> IMessagingService.SendMessage(string idFrom, string idTo,
            string content)
        {
            var userFrom = await userRepository.GetById(idFrom);
            var userTo = await userRepository.GetById(idTo);
            
            var translatedText = await translationService.Translate(content, userTo.langCode, 
                userFrom.langCode);

            var msg = new MessagePayload(new Message 
            {
                senderId = idFrom,
                senderLang = userFrom.language,
                recepientId = idTo,
                recepientLang = userTo.language,
                time = (long) (DateTime.UtcNow.Subtract(
                    new System.DateTime(1970, 1, 1))).TotalSeconds,
                content = translatedText
            });

            
            return await client.Notify(new FcmMessage(userTo.firebaseToken, 
                new FcmNotification { title = translatedText }, msg));
        }
    }
}