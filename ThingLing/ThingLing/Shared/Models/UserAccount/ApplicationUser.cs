using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models.UserAccount
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Date created")]
        public DateTimeOffset DateCreated { get; set; }
    }
}
