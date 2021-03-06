using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Multilang.Models.Db
{
    public class UpdateAccountModal
    {

        [RegularExpression(Utils.Validator.ALPHA_SPACE)]
        [JsonProperty("display_name")]
        public string displayName { get; set; }

        [JsonProperty("full_name")]
        public string fullName { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("country")]
        public string country { get; set; }

        [StringLength(64, MinimumLength=64)]
        [JsonProperty("pass_hash")]
        public string passwordHash { get; set; }

        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("language")]
        public string language { get; set; }

        [JsonProperty("firebase_token")]
        public string firebaseToken { get; set; }

        [JsonProperty("translation_engine")]
        public int translationEngine { get; set; }
    }
}