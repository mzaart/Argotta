using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Chat
{
    public class SendMessagesModel
    {
        [Required]
        [JsonProperty("recipient_ids")]
        public List<string> recipientIds { get; set; }

        [Required]
        [JsonProperty("message")]
        public string message { get; set; }

        [Required]
        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("sender_language")]
        public string senderLanguage;
    }
}