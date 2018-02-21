using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Chat
{
    public class PoorTranslationModel
    {
        [Required]
        [JsonProperty("recipient_id")]
        public string recipientId { get; set; }

        [Required]
        [JsonProperty("message")]
        public string message { get; set; }
    }
}