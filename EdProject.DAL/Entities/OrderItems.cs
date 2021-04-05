using EdProject.DAL.Entities.Base;
using EdProject.DAL.Entities.Enums;

namespace EdProject.DAL.Entities
{
    public class OrderItems : BaseEntity
    {
        public int Amount { get; set; }
        public CurrencyTypes Currency { get; set; }
        public long EditionId { get; set; }
        public long OrderId { get; set; }
        public decimal Total { get; set; }


        public Edition Edition { get; set; }
        public Orders Order { get; set; }

    }
}
