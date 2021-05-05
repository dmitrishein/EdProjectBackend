using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class AuthorViewModel
    {
        public long Id { get; set; }


        [Required]
        public string FullName { get; set; }
    }
}
