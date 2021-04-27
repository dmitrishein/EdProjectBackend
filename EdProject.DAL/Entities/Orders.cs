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
        public OrderStatusType StatusType { get; set; }

        public long UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }

        public long? PaymentId { get; set; }
        public virtual Payments Payment { get; set; }

    }
}
