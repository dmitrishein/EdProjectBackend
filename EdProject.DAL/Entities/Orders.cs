using EdProject.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Entities
{ 
    public class Orders : BaseEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public long PaymentId { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; }
        public Payments Payment { get; set; }
    }
}
