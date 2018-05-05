using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Multilang.Utils;

namespace Multilang.Models.Requests.Accounts
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(Utils.Validator.ALPHA_NUM)]
        [JsonProperty("display_name")]
        public string displayName { get; set; }

        [StringLength(64, MinimumLength=64)]
        [JsonProperty("pass_hash")]
        public string passHash { get; set; }

        [Required]
        [JsonProperty("firebase_token")]
        public string firebaseToken { get; set; }
    }
}