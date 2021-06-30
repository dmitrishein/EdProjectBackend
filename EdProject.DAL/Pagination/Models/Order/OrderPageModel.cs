using EdProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Pagination.Models.Order
{
   public class OrderPageModel
   {
        //public decimal MaxPrice { get; set; }
        //public decimal MinPrice { get; set; }
        public long TotalItemsAmount { get; set; }
        public long CurrentPage { get; set; }
        public List<Orders> OrdersPage { get; set; }
   }
}

