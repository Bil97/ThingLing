using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models.UserAccount
{
    public class ChangePassword
    {
        [Required]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm new password")]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }

    }
}
