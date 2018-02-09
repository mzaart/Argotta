using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Accounts {

    public class UpdateFirebaseTokenModel {

        [Required]
        [JsonProperty("token")]
        public string token;
    }
} 