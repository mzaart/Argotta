using Multilang.Services.MessagingServices.Payloads;
using Multilang.Models.Messages;
using Newtonsoft.Json;

namespace Multilang.Services.MessagingServices.Payloads {

    public class MessagePayload: BasePayload {
        
        private readonly string PAYLOAD_TYPE = "Message";

        [JsonProperty("messages")]
        public Message message { get; set; }

        public MessagePayload(Message message) {
            this.type = PAYLOAD_TYPE;
            this.message = message;
        }

        // empty public constructor for json parsing
        public MessagePayload() {}
    }
}