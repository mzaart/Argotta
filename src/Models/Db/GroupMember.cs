using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Db
{
    public class GroupMember
    {
        [Required]
        [JsonProperty("id")]
        public string Id;

        [Required]
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        public override bool Equals(object other)
        {
            if (!(other is GroupMember))
            {
                return false;
            }

            return this.Id == (other as GroupMember).Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}