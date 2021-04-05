using EdProject.DAL.Entities.Base;
using EdProject.DAL.Enums;
using System;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class Orders : BaseEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public long PaymentId { get; set; }
        public OrderSatusTypes MyProperty { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
        public Payments Payment { get; set; }
    }
}
