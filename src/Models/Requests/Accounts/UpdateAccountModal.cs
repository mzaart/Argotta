using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Multilang.Models.Accounts
{
    public class UpdateAccountModal
    {

        [RegularExpression(Utils.Validator.ALPHA_SPACE)]
        [JsonProperty("display_name")]
        public string displayName { get; set; }

        [StringLength(64, MinimumLength=64)]
        [JsonProperty("pass_hash")]
        public string passwordHash { get; set; }

        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("language")]
        public string language { get; set; }

        [JsonProperty("firebase_token")]
        public string firebaseToken { get; set; }
    }
}