using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models.UserAccount
{
    public class Login
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
