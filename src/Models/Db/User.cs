using System.Collections.Generic;
using Newtonsoft.Json;

namespace Multilang.Models.Db {

    public class User {

        public User() 
        {
            blockedIds = new List<string>();
            invitations = new List<Invitation>();
        }

        public string id { get; set; }
        public string language { get; set; }
        public string langCode { get; set; }
        public string displayName { get; set; }
        public string passwordHash { get; set; }
        public string firebaseToken { get; set; }
        public List<string> blockedIds { get; set; }
        public List<Invitation> invitations { get; set; }
    }
}