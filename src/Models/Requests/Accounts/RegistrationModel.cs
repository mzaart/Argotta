using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Multilang.Utils;

namespace Multilang.Models.Requests.Accounts
{
    public class RegistrationModel
    {
        [Required]
        [RegularExpression(Utils.Validator.ALPHA_SPACE)]
        [JsonProperty("display_name")]
        public string displayName { get; set; }

        [Required]
        [StringLength(64, MinimumLength=64)]
        [JsonProperty("pass_hash")]
        public string passHash { get; set; }

        [Required]
        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("language")]
        public string language { get; set; }

        [Required]
        [JsonProperty("firebase_token")]
        public string firebaseToken { get; set; }
    }
}