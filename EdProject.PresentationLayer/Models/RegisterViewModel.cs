using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }
        
        [Display(Name = "Name")]
        [Required]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
