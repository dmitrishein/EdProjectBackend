using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class AuthorInEditionViewModel
    {
        [Required]
        public long AuthorId { get; set; }
        [Required]
        public long EditionId { get; set; }
    }
}
