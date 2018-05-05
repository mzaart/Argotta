using Newtonsoft.Json;

namespace Multilang.Models.Responses.Translation
{
    public class TranslationResponse : BaseResponse
    {
        [JsonProperty("translation")]
        public string Translation { get; set; }
    }
}