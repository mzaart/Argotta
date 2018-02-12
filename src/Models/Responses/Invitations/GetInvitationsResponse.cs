using System.Collections.Generic;
using Multilang.Models.Db;
using Newtonsoft.Json;

namespace Multilang.Models.Responses.Invitations
{
    public class GetInvitationsResponse : BaseResponse
    {
        [JsonProperty("invitations")]
        public ICollection<Invitation> invitations;
    }
}