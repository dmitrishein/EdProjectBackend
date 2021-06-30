using EdProject.DAL.Entities.Base;
using EdProject.DAL.Entities.Enums;

namespace EdProject.DAL.Entities
{
    public class Payments:BaseEntity
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public CurrencyTypes Currency { get; set; }
        public virtual Orders Order { get; set; }
    }
}
