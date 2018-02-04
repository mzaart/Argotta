using Newtonsoft.Json;

namespace Multilang.Services.MessagingServices.Firebase {

    public class FcmNotification {

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("text", NullValueHandling=NullValueHandling.Ignore)]
        public string text { get; set; }

        [JsonProperty("click_action", NullValueHandling=NullValueHandling.Ignore)]
        public string clickAction { get; set; }

        [JsonProperty("sound", NullValueHandling=NullValueHandling.Ignore)]
        public string sound { get; set; }
    }
}