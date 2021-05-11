using EdProject.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.Orders
{
    public class OrderItemModel
    {
        public long EditionId { get; set; }
        public long OrderId { get; set; }
    }
}
