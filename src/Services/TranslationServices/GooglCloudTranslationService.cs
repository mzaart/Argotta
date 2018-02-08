using Google.Cloud.Translation.V2;
using Google.Apis.Auth.OAuth2;
using Multilang.Services.ConfigurationServices;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Multilang.Services.TranslationServices
{
    public class GoogleCloudTranslationService : ITranslationService
    {
        private readonly IConfigService config;

        public GoogleCloudTranslationService(IConfigService config) {
            this.config = config;
        }

        async Task<string> ITranslationService.Translate(string text, string targetLanguageCode, 
            string sourceLanguageCode)
        {
            return await Task.Run( () => {
                // TODO make this async
                string json = File.ReadAllText(config.GetGoogleCloudAccountFilePath());
                var credential = GoogleCredential.FromJson(json);
                TranslationClient client = TranslationClient.Create(credential);
                var response = client.TranslateText(text, targetLanguageCode,
                    sourceLanguageCode);

                return response.TranslatedText;
            });
        }
    }
}