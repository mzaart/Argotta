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
        public string issuedAt;

        [JsonProperty("id")]
        public string id;
    }
}