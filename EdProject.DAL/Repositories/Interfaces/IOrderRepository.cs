using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Orders>
    {
        public Task AddItemListToOrderAsync(Orders order, List<OrderItem> items);
        public Task RemoveOrderByIdAsync(long id);
        public Task RemoveItemListFromOrderAsync(Orders order, List<OrderItem> items);
        public Task ClearOrderByIdAsync(Orders order);
        public Task UpdateOrderItem(Orders order, OrderItem orderItem);
        public Task<List<Orders>> GetOrderByUserIdAsync(long userId);
        public Task<List<Orders>> GetAllOrdersAsync();
        public Task<List<Orders>> OrdersPage(int pageNumber, int pageSize, string searchString);
        public decimal GetOrderCost(Orders order);
    }
}
