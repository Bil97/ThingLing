using System;
using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models.UserAccount
{
    public class ContactForm
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public string Reply { get; set; }

        public DateTimeOffset DateSent { get; set; } = DateTimeOffset.Now;
    }
}
