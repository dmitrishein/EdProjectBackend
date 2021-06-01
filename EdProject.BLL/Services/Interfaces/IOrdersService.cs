using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IOrdersService 
    {
        public Task CreateOrderAsync(OrderModel orderModel);
        public Task CreatePaymentAsync(PaymentModel paymentModel);
        public Task CreateItemsInOrderAsync(List<OrderItemModel> orderItemlistModel);
        public Task<List<OrderModel>> GetOrdersByUserIdAsync(long userId);
        public Task<List<OrderModel>> GetOrdersPageAsync(FilterPageModel pageModel);
        public Task<List<OrderModel>> GetOrdersListAsync();
        public Task<OrderModel> GetOrderByIdAsync(long orderId);
        public Task<List<OrderItemModel>> GetItemsInOrderAsync(long orderId);
        public Task UpdateOrderItemAsync(OrderItemModel orderItem);
        public Task RemoveOrderByIdAsync(long orderId);
        public Task ClearOrder(long orderId);
    }
}
