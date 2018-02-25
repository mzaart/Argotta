using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Multilang.Models.Db;

namespace Multilang.Models.FirebasePayloads
{
    public class InvitationPayload : BasePayload
    {
        [Required]
        [JsonProperty("invitation")]
        public Invitation invitation { get; set; }
        
        [Required]
        [JsonProperty("sender_display_name")]
        public string senderDisplayName { get; set; }

        [Required]
        [JsonProperty("sender_lang")]
        public string senderLang { get; set; }

        public InvitationPayload(Invitation invitation, string senderDisplayName, string sender_lang)
        {
            this.type = "Invitation";
            this.invitation = invitation;
            this.senderDisplayName = senderDisplayName;
            this.senderLang = sender_lang;
        }
    }
}