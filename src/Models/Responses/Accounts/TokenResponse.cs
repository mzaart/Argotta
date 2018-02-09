using Newtonsoft.Json;

namespace Multilang.Models.Responses.Accounts
{
    public class TokenResponse : BaseResponse
    {
        [JsonProperty("token")]
        public string token;

        [JsonProperty("language")]
        public string language;
    }
}