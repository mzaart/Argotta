using Newtonsoft.Json;

namespace Multilang.Models.Jwt
{
    public class JwtBody
    {
        [JsonProperty("iat")]
        public string issuedAt;

        [JsonProperty("id")]
        public string id;
    }
}