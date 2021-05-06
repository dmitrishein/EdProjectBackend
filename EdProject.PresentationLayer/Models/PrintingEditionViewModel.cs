using EdProject.DAL.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class PrintingEditionViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public EditionStatusTypes Status { get; set; }
        [Required]
        public CurrencyTypes Currency { get; set; }
        [Required]
        public EditionStatusTypes Type { get; set; }
    }
}
