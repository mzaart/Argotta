using Newtonsoft.Json;

namespace Multilang.Models.Requests {

    public class UpdateFirebaseTokenModel {

        [JsonProperty("id")]
        public string id;

        [JsonProperty("token")]
        public string token;
    }
} 