using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class UserCreateViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
