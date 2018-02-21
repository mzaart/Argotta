using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Chat
{
    public class SendBinaryModel
    {
        [Required]
        [JsonProperty("recipient_id")]
        public string recipientId { get; set; }

        [Required]
        [JsonProperty("base64_data")]
        public string base64Data { get; set; }

        [Required]
        [JsonProperty("file_name")]
        public string fileName { get; set; }

        [Required]
        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("sender_language")]
        public string senderLanguage;
    }
}