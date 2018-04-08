using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Groups
{
    public class SendMessageRequest
    {
        [Required]
        [RegularExpression(Utils.Validator.ALPHA_NUM)]
        [JsonProperty("title")]
        public string Title { get; set; }

        [Required]
        [JsonProperty("message")]
        public string message { get; set; }

        [Required]
        [RegularExpression(Utils.Validator.ALPHA)]
        [JsonProperty("sender_language")]
        public string senderLanguage;
    }
}