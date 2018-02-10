using Newtonsoft.Json;

namespace Multilang.Models.Responses.Accounts
{
    public class ProfilePicResponse : BaseResponse
    {
        [JsonProperty("pic_url")]
        public string picUrl;
    }
}