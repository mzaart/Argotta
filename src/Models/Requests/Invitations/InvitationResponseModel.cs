using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Invitations
{
    public class InvitationResponseModel
    {
        [Required]
        [JsonProperty("invitation_id")]
        public string invitationId  { get; set; }

        [Required]
        [JsonProperty("accepted")]
        public bool accepted { get; set; }
    }
}