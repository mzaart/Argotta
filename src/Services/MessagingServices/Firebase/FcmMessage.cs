using Newtonsoft.Json;
using Multilang.Services.MessagingServices.Payloads;
using System;

namespace Multilang.Services.MessagingServices.Firebase {

    public class FcmMessage {

        [JsonProperty("to")]
        public string token { get; set; }

        [JsonProperty("data", NullValueHandling=NullValueHandling.Ignore)]
        public BasePayload data { get; set; }

        [JsonProperty("notification", NullValueHandling=NullValueHandling.Ignore)]
        public FcmNotification notification { get; set; }

        public FcmMessage(string token, BasePayload data)
            : this(token, null, data) {}

        public FcmMessage(string token, FcmNotification notification)
            : this(token, notification, null) {}
        
        public FcmMessage(string token, FcmNotification notification, BasePayload data) {
            this.token = token;
            this.notification = notification;
            this.data = data;
        }

        // public empty constructor for json parsing
        public FcmMessage() {}

        public string toJson() {
            Console.WriteLine(JsonConvert.SerializeObject(this));
            return JsonConvert.SerializeObject(this);
        }
    }
}