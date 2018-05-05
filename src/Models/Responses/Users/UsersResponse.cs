using System.Collections.Generic;
using Newtonsoft.Json;

namespace Multilang.Models.Responses.Users
{
    public class UsersResponse
    {
        [JsonProperty("users")]
        public List<UserResponse> Users { get; set; }
    }
}