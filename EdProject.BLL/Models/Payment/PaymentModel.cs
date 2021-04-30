using EdProject.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.Payment
{
    public class PaymentModel
    {
        public long Amount { get; set; }
        public CurrencyTypes currency { get; set; }
        public string TransactionId { get; set; }
    }
}
