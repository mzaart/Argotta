using Newtonsoft.Json;

namespace Multilang.Models.Responses {

    public class BaseResponse {
        
        [JsonProperty("succeeded")]
        bool succeeded { get; set; }

        [JsonProperty("reason", NullValueHandling=NullValueHandling.Ignore)]
        string reason;

        public BaseResponse(bool succeeded) : this(succeeded, null) {}

        public BaseResponse(bool succeeded, string reason) {
            this.succeeded = succeeded;
            this.reason = reason;
        }

        public BaseResponse() {}
    }
}