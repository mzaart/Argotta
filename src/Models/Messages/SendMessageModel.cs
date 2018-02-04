using Newtonsoft.Json;

namespace Multilang.Models.Messages {

    public class SendMessageModel {

        [JsonProperty("sender_id")]
        public string senderId { get; set; }

        [JsonProperty("recepient_id")]
        public string recepientId { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }
}