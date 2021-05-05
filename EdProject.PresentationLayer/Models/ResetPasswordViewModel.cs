using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password aren't equal")]
        [Required]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
