
namespace Multilang.Models.FirebasePayloads {

    /// <summary> Acts as a base class for all Firebase payloads.</summary>
    public class BasePayload {

        /// <summary>
        /// Indicates the type of the payload. This values is to be
        /// determined by child classes.
        /// </summary>
        public string type { get; set; }
    }
}