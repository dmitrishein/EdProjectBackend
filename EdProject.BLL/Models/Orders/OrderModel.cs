using EdProject.DAL.Entities;
using EdProject.DAL.Enums;
using System.Collections.Generic;

namespace EdProject.BLL.Models.Orders
{
    public class OrderModel : BaseModel
    {
        public long UserId { get; set; }
        public string Description { get; set; }
        public string StatusType { get; set; }
        
    }
}
