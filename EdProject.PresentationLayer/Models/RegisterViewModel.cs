using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required]
        public string Username { get; set; }
        
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords doesn't match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
