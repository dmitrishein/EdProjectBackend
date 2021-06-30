using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.User;
using EdProject.DAL.Pagination.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IOrdersService 
    {
        public Task<long> CreateOrderAsync(string token, OrderCreateModel orderCreateModel);
        public Task<List<OrderModel>> GetOrdersByUserIdAsync(long userId);
        public Task<OrdersPageResponseModel> GetOrdersPageAsync(string token,OrdersPageParameters pageModel);
        public Task<List<OrderModel>> GetOrdersListAsync();
        public Task<OrderModel> GetOrderByIdAsync(long orderId);
        public Task<List<OrderItemModel>> GetItemsInOrderAsync(long orderId);
        public Task UpdateOrderItemAsync(OrderItemModel orderItem);
        public Task RemoveOrderByIdAsync(long orderId);
        public Task ClearOrder(long orderId);
    }
}
