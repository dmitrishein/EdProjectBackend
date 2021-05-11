using EdProject.DAL.Entities.Enums;

namespace EdProject.BLL.Models.Payment
{
    public class PaymentModel
    {
        public long Amount { get; set; }
        public CurrencyTypes Currency { get; set; }
        public string TransactionId { get; set; }
        public long OrderId { get; set; }
    }
}
