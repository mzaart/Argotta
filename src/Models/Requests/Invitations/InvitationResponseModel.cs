using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Invitations
{
    public class InvitationResponseModel
    {
        [Required]
        [JsonProperty("sender_id")]
        public string senderId  { get; set; }

        [Required]
        [JsonProperty("accepted")]
        public bool accepted { get; set; }
    }
}