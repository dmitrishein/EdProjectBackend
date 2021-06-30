using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Enums;

namespace EdProject.DAL.Models
{
    public class EditionPageParameters
    {
        public int ElementsPerPage { get; set; }
        public int CurrentPageNumber { get; set; }
        public string SearchString { get; set; }
        public decimal MaxUserPrice { get; set; }
        public decimal MinUserPrice { get; set; }
        public EditionTypes[] EditionTypes { get; set; }
        public AvailableStatusType isAvailable { get; set; }
        public EditionSortingType SortType { get; set; }
        public bool IsReversed { get; set; }


    }
}
