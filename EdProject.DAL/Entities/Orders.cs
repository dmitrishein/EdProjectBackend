using EdProject.DAL.Entities.Base;
using EdProject.DAL.Enums;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class Orders : BaseEntity
    {
        public string Description { get; set; }
        public decimal Total { get; set; }
        public PaidStatusType StatusType { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Edition> Editions { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

        public long? PaymentId { get; set; }
        public virtual Payments Payment { get; set; }

    }
}
