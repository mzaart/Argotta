using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Multilang.Models.Translation;
using Multilang.Services.ConfigurationServices;
using Newtonsoft.Json;

namespace Multilang.Services.TranslationServices
{
    public class AzureTranslationService : ITranslationService
    {
        private IConfigService config;
        private HttpClient client;

        public AzureTranslationService(IConfigService config, HttpClient client)
        {
            this.config = config;
            this.client = client;
        }

        public async Task<string> Translate(string text, string targetLanguageCode,
            string sourceLanguageCode)
        {
            var url = "https://api.cognitive.microsofttranslator.com/translate";
            var query = $"translate?api-version=3.0&from={sourceLanguageCode}&to={targetLanguageCode}";
            
            
            Object[] body = new Object[] { new { Text =  text } };
            var reqBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(url+query);
                request.Content = new StringContent(reqBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", config.GetAzureTranslationKey());

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<TranslationResponse[]>(responseBody);

                return responseObj[0].TranslatedItems[0].Text;     
            }
        }
    }
}