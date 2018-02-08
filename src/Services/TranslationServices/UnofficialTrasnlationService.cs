using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Multilang.Services.TranslationServices {

    public class UnofficialTranslationService : ITranslationService
    {
        private readonly string REGEX = "\"(.*?)\"";
        private readonly string END_POINT = 
            "https://translate.googleapis.com/translate_a/single";

        private HttpClient httpClient;

        public UnofficialTranslationService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        async Task<string> ITranslationService.Translate(string text, string targetLanguageCode, 
            string sourceLanguageCode)
        {
            var args = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"client", "gtx"},
                {"sl", sourceLanguageCode},
                {"tl", targetLanguageCode},
                {"dt", "t"},
                {"q", text}
            });

            var response = await httpClient.PostAsync(END_POINT, args);
            StreamReader readStream = new StreamReader(
                response.Content.ReadAsStreamAsync().Result, Encoding.UTF8);
            string responeText = readStream.ReadToEnd();

            Regex regex = new Regex(REGEX);
            var translated = regex.Matches(responeText)[0].Value;
            return translated.Substring(1, translated.Length-2);
        }
    }
}