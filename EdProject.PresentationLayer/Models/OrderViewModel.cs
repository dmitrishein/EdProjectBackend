using EdProject.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Models
{
    public class OrderViewModel
    {
        public long UserId { get; set; }
        public string Description { get; set; }
        public long PaymentId { get; set; }
        public OrderStatusType StatusType { get; set; }
    }
}
