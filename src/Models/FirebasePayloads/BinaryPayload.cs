using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.FirebasePayloads
{
    public class BinaryPayload : BasePayload
    {
        [Required]
        [JsonProperty("sender_id")]
        public string senderId { get; set; }

        [Required]
        [JsonProperty("base64_data")]
        public string base64Data { get; set; }

        [Required]
        [JsonProperty("file_name")]
        public string fileName { get; set; }

        public BinaryPayload(string senderId, string base64Data, string fileName) 
        {
            this.type = "binary";
            this.senderId = senderId;
            this.base64Data = base64Data;
            this.fileName = fileName;
        }
    }
}