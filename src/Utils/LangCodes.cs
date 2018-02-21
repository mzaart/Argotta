using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Multilang.Utils
{
    public class LangCodes
    {
        private Dictionary<string, string> langCodes;

        public LangCodes(IHostingEnvironment environment)
        {
            string path = Path.Combine(environment.ContentRootPath, "lang_codes.json");
            string json = File.ReadAllText(path);
            this.langCodes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public string GetCode(string language)
        {
            if (!langCodes.ContainsKey(language)) {
                return "en";
            }
            return langCodes[language];
        }
    }
}