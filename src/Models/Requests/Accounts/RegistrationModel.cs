using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Multilang.Utils;

namespace Multilang.Models.Requests.Accounts
{
    public class RegistrationModel
    {
        [Required]
        [CustomValidation(typeof(Utils.Validator), "AlphaSpace")]
        [JsonProperty("display_name")]
        public string displayName;

        [Required]
        [StringLength(64)]
        [JsonProperty("pass_hash")]
        public string passHash;

        [Required]
        [CustomValidation(typeof(Utils.Validator), "Alpha")]
        [JsonProperty("language")]
        public string language;

        [Required]
        [JsonProperty("firebase_token")]
        public string firebaseToken;
    }
}