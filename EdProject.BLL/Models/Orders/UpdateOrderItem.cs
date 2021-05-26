namespace EdProject.BLL.Models.Orders
{
    public class UpdateOrderItem
    {
        public long OrderId { get; set; }
        public long EditionId { get; set; }
        public int ItemsCount { get; set; }
    }
}
