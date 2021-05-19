using EdProject.DAL.Enums;

namespace EdProject.BLL.Models.Orders
{
    public class OrderModel : BaseModel
    {
        public long UserId { get; set; }
        public string Description { get; set; }
        public PaidStatusType StatusType { get; set; }
        
    }
}
