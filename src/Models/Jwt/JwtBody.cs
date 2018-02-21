using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Multilang.Models.Jwt
{
    [BindNever]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class JwtBody
    {
        [Required]
        [JsonProperty("iat")]
        public string issuedAt { get; set; }

        [Required]
        [JsonProperty("id")]
        public string id { get; set; }

        [Required]
        [JsonProperty("display_name")]
        public string displayName { get; set; }

        [Required]
        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("lang_code")]
        public string langCode { get; set; }

        [Required]
        [RegularExpression(Utils.Validator.ALPHA)]
        public string language { get; set; }
    }
}