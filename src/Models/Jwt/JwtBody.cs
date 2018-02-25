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
        [JsonProperty("iat")]
        public string issuedAt { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("display_name")]
        public string displayName { get; set; }

        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("lang_code")]
        public string langCode { get; set; }

        [RegularExpression(Utils.Validator.ALPHA)]
        public string language { get; set; }
    }
}