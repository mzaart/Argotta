using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Multilang.Models.Db
{
    public class Group
    {
        [Required]
        [Key]
        public string Title { get; set; }

        [Required]
        public string AdminId { get; set; }

        [Obsolete]
        [Required]
        public string members
        {
            get
            {
                return JsonConvert.SerializeObject(membersList);
            }

            set
            {
                membersList = JsonConvert.DeserializeObject<ISet<GroupMember>>(value);
            }
        }

        [NotMapped]
        public ISet<GroupMember> membersList { get; set; }

        public Group()
        {
            membersList = new HashSet<GroupMember>();
        }
    }
}