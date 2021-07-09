using EdProject.DAL.Entities;
using System.Collections.Generic;

namespace EdProject.DAL.Pagination.Models.Order
{
    public class OrderPageModel
   {
        public long TotalItemsAmount { get; set; }
        public long CurrentPage { get; set; }
        public List<Orders> OrdersPage { get; set; }
   }
}

