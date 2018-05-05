using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Requests.Translations
{
    public class TranslationRequest
    {
        [JsonProperty("lang_code_from")]
        [Required]
        public string From { get; set; }

        [JsonProperty("lang_code_to")]
        [Required]
        public string To { get; set; }

        [JsonProperty("text")]
        [Required]
        public string Text { get; set; }
    }
}