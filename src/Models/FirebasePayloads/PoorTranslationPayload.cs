using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.FirebasePayloads
{
    public class PoorTrasnlationPayload : BasePayload
    {
        [Required]
        [JsonProperty("display_name")]
        public string displayName { get; set; }

        [Required]
        [JsonProperty("language")]
        public string language { get; set; }

        [Required]
        [JsonProperty("message")]
        public string message { get; set; }

        public PoorTrasnlationPayload(string displayName, string language, string message)
        {
            this.type = "PoorTranslation";
            this.displayName = displayName;
            this.language = language;
            this.message = message;
        }
    }
}