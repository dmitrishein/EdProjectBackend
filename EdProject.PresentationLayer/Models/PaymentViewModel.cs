using EdProject.DAL.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class PaymentViewModel
    {
        [Required]
        public long Amount { get; set; }
        public CurrencyTypes currency { get; set; } 
    }
}
