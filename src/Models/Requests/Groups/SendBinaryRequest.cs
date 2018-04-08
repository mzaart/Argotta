using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Groups
{
    public class SendBinaryRequest
    {
        [Required]
        [RegularExpression(Utils.Validator.ALPHA_NUM)]
        [JsonProperty("title")]
        public string Title { get; set; }

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