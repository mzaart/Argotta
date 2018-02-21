using Newtonsoft.Json;
using Multilang.Models.FirebasePayloads;
using System;
using System.Collections.Generic;

namespace Multilang.Services.MessagingServices.Firebase {

    public class FcmMessage {

        [JsonProperty("to")]
        public string token { get; set; }

        [JsonProperty("registration_ids")]
        public List<string> registrationIds { get; set; }

        [JsonProperty("data", NullValueHandling=NullValueHandling.Ignore)]
        public BasePayload data { get; set; }

        [JsonProperty("notification", NullValueHandling=NullValueHandling.Ignore)]
        public FcmNotification notification { get; set; }
    }
}