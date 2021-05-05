using EdProject.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace EdProject.PresentationLayer.Models
{
    public class OrderViewModel
    {
        [Required]
        public long UserId { get; set; }
        public string Description { get; set; }
        public OrderStatusType StatusType { get; set; }
    }
}
