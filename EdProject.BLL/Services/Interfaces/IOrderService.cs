using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IOrderService 
    {
        public Task CreateOrderAsync(OrderModel orderModel);
        public Task CreateOrderItemAsync(OrderItemModel orderModel);
        public Task CreatePaymentAsync(PaymentModel paymentModel);
        public IQueryable<Orders> GetOrdersListByUserId(long userId);
    }
}
