using Newtonsoft.Json;

namespace Multilang.Models.Messages {

    public class Message {

        [JsonProperty("sender_id")]
        public string senderId { get; set; }

        [JsonProperty("sender_username")]
        public string senderUsername { get; set; }

        [JsonProperty("sender_lang_code")]
        public string senderLangCode { get; set; }

        [JsonProperty("recepient_id")]
        public string recepientId { get; set; }

        [JsonProperty("recepient_lang")]
        public string recepientLang { get; set; }

        [JsonProperty("time")]
        public long time { get; set; } // unix time

        [JsonProperty("content")]
        public string content { get; set; }
    }
}