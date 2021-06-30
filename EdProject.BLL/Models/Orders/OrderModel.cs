using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.Orders
{
    public class OrderModel : BaseModel
    {
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public string StatusType { get; set; }
    }
}
