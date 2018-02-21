using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Multilang.Utils;

namespace Multilang.Models.Requests.Accounts
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(Utils.Validator.ALPHA_SPACE)]
        public string displayName { get; set; }

        [StringLength(64, MinimumLength=64)]
        [JsonProperty("pass_hash")]
        public string passHash { get; set; }
    }
}