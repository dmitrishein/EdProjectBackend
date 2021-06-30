using EdProject.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Pagination.Models
{
    public class OrdersPageParameters
    {
        public int ElementsPerPage { get; set; }
        public int CurrentPageNumber { get; set; }
        public decimal MaxUserPrice { get; set; }
        public decimal MinUserPrice { get; set; }
        public string SearchString { get; set; }
        public PaidStatusType[] PaidStatus { get; set; }
        public string UserId { get; set; }
        //public EditionSortingType SortType { get; set; }
        //public bool IsReversed { get; set; }
    }
}
