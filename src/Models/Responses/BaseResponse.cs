using Newtonsoft.Json;

namespace Multilang.Models.Responses {

    public class BaseResponse {
        
        [JsonRequired]
        [JsonProperty("succeeded")]
        public bool succeeded { get; set; }

        [JsonProperty("reason", NullValueHandling=NullValueHandling.Ignore)]
        public string reason;

        public BaseResponse(bool succeeded) : this(succeeded, null) {}

        public BaseResponse(bool succeeded, string reason) {
            this.succeeded = succeeded;
            this.reason = reason;
        }

        public BaseResponse() {}
    }
}