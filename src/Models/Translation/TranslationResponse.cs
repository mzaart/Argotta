using Newtonsoft.Json;

namespace Multilang.Models.Translation
{
    public class TranslationResponse
    {
        [JsonProperty("translations")]
        public Translation[] TranslatedItems { get; set; }
    }

    public class Translation
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}