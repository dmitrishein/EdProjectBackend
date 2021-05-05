using EdProject.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Models
{
    public class OrderItemViewModel
    {
        [Required]
        public int Amount { get; set; }
        public CurrencyTypes Currency { get; set; } = CurrencyTypes.UAH;
        public long EditionId { get; set; }
        [Required]
        public long OrderId { get; set; }
    }
}
