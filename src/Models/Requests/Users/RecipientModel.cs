using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Users
{
    public class RecipientModel
    {
        [Required]
        [JsonProperty("recipient_id")]
        public string recipientId { get; set; }
    }
}