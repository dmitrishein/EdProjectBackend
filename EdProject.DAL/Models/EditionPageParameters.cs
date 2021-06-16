using EdProject.DAL.Entities.Enums;

namespace EdProject.DAL.Models
{
    public class EditionPageParameters
    {
        public int ElementsAmount { get; set; }
        public int PageNumber { get; set; }
        public string SearchString { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public EditionTypes[] editionTypes { get; set; }
        public AvailableStatusType isAvailable { get; set; }
    }
}
