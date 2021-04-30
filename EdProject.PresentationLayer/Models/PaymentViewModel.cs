using EdProject.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Models
{
    public class PaymentViewModel
    {
        public long Amount { get; set; }
        public CurrencyTypes currency { get; set; } 
    }
}
