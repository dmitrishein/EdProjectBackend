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
        public Task UpdateOrderAsync(string token, OrderUpdateModel orderUpdate);
        public Task<OrdersPageResponseModel> GetOrdersPageAsync(string token,OrdersPageParameters pageModel);
        public Task RemoveOrderByIdAsync(long orderId);
    }
}
