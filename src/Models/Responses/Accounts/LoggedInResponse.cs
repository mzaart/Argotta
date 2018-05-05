using Newtonsoft.Json;

namespace Multilang.Models.Responses.Accounts
{
    public class LoggedInResponse : BaseResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("lang")]
        public string language { get; set; }

        [JsonProperty("lang_code")]
        public string langCode { get; set; }

        [JsonProperty("display_name")]
        public string displayName { get; set; }

        [JsonProperty("full_name")]
        public string fullName { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("token")]
        public string token { get; set; }
    }
}