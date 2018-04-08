using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Multilang.Models.Db;
using Newtonsoft.Json;

namespace Multilang.Models.Responses.Groups
{
    public class GetMembersResponse
    {
        [Required]
        [JsonProperty("members")]
        public ISet<GroupMember> members { get; set; }
    }
}