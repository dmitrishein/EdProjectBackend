using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.PrintingEditions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IOrdersService 
    {
        public Task CreateOrderAsync(OrderModel orderModel);
        public Task CreateItemInOrderAsync(OrderItemModel orderModel);
        public Task CreatePaymentAsync(PaymentModel paymentModel);
        public Task CreateItemsListInOrderAsync(OrderItemsListModel orderItemlistModel);
        public Task<List<OrderModel>> GetOrdersByUserIdAsync(long userId);
        public Task<List<OrderModel>> GetOrdersPageAsync(PageModel pageModel);
        public Task<List<OrderModel>> GetOrdersListAsync();
        public Task<OrderModel> GetOrderByIdAsync(long orderId);
        public Task<List<EditionModel>> GetItemsInOrderAsync(long orderId);
        public Task<PaymentModel> GetPaymentInOrderAsync(long orderId);
        public Task RemoveItemFromOrderAsync(OrderItemModel orderItemModel);
        public Task RemoveItemsListFromOrder(OrderItemsListModel orderItemsListModel);
    }
}
