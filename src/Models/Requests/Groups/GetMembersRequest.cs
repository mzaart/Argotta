using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Groups
{
    public class GetMembersRequest
    {
        [Required]
        [RegularExpression(Utils.Validator.ALPHA_NUM)]
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}