using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Multilang.Models.Db;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Groups
{
    public class AddMembersRequest
    {
        [RegularExpression(Utils.Validator.ALPHA_NUM)]
        [JsonProperty("tite")]
        public string Title { get; set; }

        [Required]
        [JsonProperty("members")]
        public ISet<GroupMember> Members { get; set; }
    }
}