using Newtonsoft.Json;

namespace Multilang.Models.Requests.Accounts
{
    public class SetProfilePicModal
    {
        [JsonProperty("user_id")]
        public string userId;

        [JsonProperty("pic_base64")]
        public string picBase64;
    }
}