using System.Collections.Generic;
using Newtonsoft.Json;

namespace Multilang.Models.Accounts {

    public class User {

        public User() 
        {
            blockedIds = new List<string>();
        }

        public string id { get; set; }
        public string language { get; set; }
        public string langCode { get; set; }
        public string displayName { get; set; }
        public string passwordHash { get; set; }
        public string firebaseToken;
        public List<string> blockedIds;
    }
}