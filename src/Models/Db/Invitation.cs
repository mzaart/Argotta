using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Multilang.Models.Db
{
    public class Invitation : IValidatableObject
    {

        // for testing purposes only
        private static int maxId = 0;

        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty("accepted")]
        public Boolean accepted { get; set; }

        [JsonProperty("sender_id")]
        public string senderId { get; set; }

        [Required]
        [RegularExpression(Utils.Validator.ALPHA_SPACE)]
        [JsonProperty("sender_display_name")]
        public string senderDisplayName { get; set; }

        [Required]
        [JsonProperty("is_group")]
        public bool isGroup { get { return groupTitle == null; } }

        [JsonProperty("group_id")]
        public string groupId { get; set; }

        [JsonProperty("group_title")]
        public string groupTitle { get; set; }

        [Required]
        [JsonProperty("recipient_id")]
        public string recipientId { get; set; }

        public Invitation()
        {
            this.Id = maxId++.ToString();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (isGroup &&  (groupId == null || groupTitle == null))
            {
                yield return new ValidationResult("Group information not present");
            }
            else if (senderId == null || senderDisplayName == null)
            {
                yield return new ValidationResult("Sender information not present");
            }
            else
            {
                yield return ValidationResult.Success;
            }
        }
    }
}