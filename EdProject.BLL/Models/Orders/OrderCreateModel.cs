using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;

namespace EdProject.BLL.Models.Orders
{
    public class OrderCreateModel
    {
        public string SourceId { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
