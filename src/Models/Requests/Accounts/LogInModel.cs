using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Multilang.Utils;

namespace Multilang.Models.Requests.Accounts
{
    public class LoginModel
    {
        
        [Required]
        [CustomValidation(typeof(Utils.Validator), "AlphaSpace")]
        public string displayName;

        [Required]
        [StringLength(64)]
        [JsonProperty("pass_hash")]
        public string passHash;

        [Required]
        [JsonProperty("firebase_token")]
        public string firebaseToken;
    }
}