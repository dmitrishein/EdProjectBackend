using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.Orders
{
    public class OrdersPageResponseModel
    {
        public long TotalItemsAmount { get; set; }
        public long CurrentPage { get; set; }
        public List<OrderModel> OrdersPage { get; set; }
    }
}
