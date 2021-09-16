using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models.UserAccount
{
    public class ForgotPassword
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
