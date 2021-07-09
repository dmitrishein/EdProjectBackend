using EdProject.DAL.Enums;

namespace EdProject.DAL.Pagination.Models
{
    public class OrdersPageParameters
    {
        public int ElementsPerPage { get; set; }
        public int CurrentPageNumber { get; set; }
        public string SearchString { get; set; }
        public string UserId { get; set; }
        public OrderSortTypes SortType { get; set; }
        public bool IsReversed { get; set; }
    }
}
