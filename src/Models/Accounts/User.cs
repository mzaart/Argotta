using Newtonsoft.Json;

namespace Multilang.Models.Accounts {

    public class User {

        public string id { get; set; }
        public string language { get; set; }
        public string langCode { get; set; }
        public string name { get; set; }
        public string passwordHash { get; set; }
        public string firebaseToken;
    }
}