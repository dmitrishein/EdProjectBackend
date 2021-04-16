using EdProject.DAL.Entities.Enums;

namespace EdProject.PresentationLayer.Models
{
    public class PrintingEditionViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public EditionStatusTypes Status { get; set; }
        public CurrencyTypes Currency { get; set; }
        public EditionStatusTypes Types { get; set; }
    }
}
