using ThingLing.Shared.Models.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models.UserAccount
{
    public class UserDetails
    {
        public string Id { get; set; }

        public string Email { get; set; }

        [Display(Name = "Date created")]
        public DateTimeOffset DateCreated { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
