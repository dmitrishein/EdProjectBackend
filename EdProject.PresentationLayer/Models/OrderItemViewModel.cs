using EdProject.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Models
{
    public class OrderItemViewModel
    {
        public int Amount { get; set; }
        public CurrencyTypes Currency { get; set; } = CurrencyTypes.UAH;
        public long EditionId { get; set; }
        public long OrderId { get; set; }
    }
}
