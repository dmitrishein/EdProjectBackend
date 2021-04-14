using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Models
{
    public class ResetPasswordModel
    {

        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password aren't equal")]
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
