using EdProject.DAL.Entities;
using EdProject.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.Orders
{
    public class OrderModel:BaseModel
    {
        public long UserId { get; set; }
        public string Description { get; set; }
        public OrderStatusType StatusType { get; set; }
        
    }
}
