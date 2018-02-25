using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace Multilang.Models.Db {

    public class User {

        public User() 
        {
            blockedIds = new List<string>();
            invitations = new List<Invitation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [RegularExpression(Utils.Validator.ALPHA)]
        public string language { get; set; }

        [RegularExpression(Utils.Validator.ALPHA)]
        public string langCode { get; set; }

        [RegularExpression(Utils.Validator.ALPHA_SPACE)]
        public string displayName { get; set; }

        [RegularExpression(Utils.Validator.ALPHA_SPACE)]
        public string fullName { get; set; }

        public string email { get; set; }
        
        [Required]
        [StringLength(64, MinimumLength=64)]
        public string passwordHash { get; set; }

        [Required]
        public string firebaseToken { get; set; }

        [Obsolete]
        [Required]
        public string blobkedIdsJson 
        {
            get 
            {
                return JsonConvert.SerializeObject(blockedIds);
            }   

            set 
            {
                blockedIds = JsonConvert.DeserializeObject<List<string>>(value);
            }
        }


        [NotMapped]
        public List<string> blockedIds { get; set; }

        [Obsolete]
        [Required]
        public string invitationsJson 
        {
            get
            {
                return JsonConvert.SerializeObject(invitations);
            }

            set
            {
                invitations = JsonConvert.DeserializeObject<List<Invitation>>(value);
            }
        }

        [NotMapped]
        public List<Invitation> invitations { get; set; }
    }
}