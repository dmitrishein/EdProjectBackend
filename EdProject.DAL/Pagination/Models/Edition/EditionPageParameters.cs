using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Enums;

namespace EdProject.DAL.Models
{
    public class EditionPageParameters
    {
        public int ElementsPerPage { get; set; }
        public int CurrentPageNumber { get; set; }
        public string SearchString { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public EditionTypes[] EditionTypes { get; set; }
        public AvailableStatusType isAvailable { get; set; }
        public SortingType SortType { get; set; }
        public bool IsReversed { get; set; }


    }
}
