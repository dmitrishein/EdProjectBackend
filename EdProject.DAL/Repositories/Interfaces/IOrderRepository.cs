using EdProject.DAL.Entities;
using EdProject.DAL.Pagination.Models;
using EdProject.DAL.Pagination.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Orders>
    {
        public Task<List<Orders>> GetOrdersByUserIdAsync(long userId);
        public Task<List<Orders>> GetAllOrdersAsync();
        public Task<OrderPageModel> OrdersPage(OrdersPageParameters pageParameters);
    }
}
