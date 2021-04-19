using EdProject.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Entities
{
    public class Payments:BaseEntity
    {
        public long TransactionId { get; set; }

        public Orders Order { get; set; }
    }
}
