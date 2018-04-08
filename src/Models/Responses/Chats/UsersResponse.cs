using System.Collections.Generic;
using Multilang.Models.Responses;
using Newtonsoft.Json;

public class UsersResponse : BaseResponse
{
    [JsonProperty("users")]
    public List<User> Users { get; set; }

    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set;}
    }
}