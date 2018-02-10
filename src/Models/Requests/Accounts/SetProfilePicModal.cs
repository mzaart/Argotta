using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Accounts
{
    public class SetProfilePicModal
    {
        [Required]
        [JsonProperty("user_id")]
        public string userId { get; set; }

        [Required]
        [JsonProperty("pic_base64")]
        public string picBase64 { get; set; }
    }
}