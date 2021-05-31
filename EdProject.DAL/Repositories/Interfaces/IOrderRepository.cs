using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Orders>
    {
        public Task<List<Orders>> GetOrdersByUserIdAsync(long userId);
        public Task<List<Orders>> GetAllOrdersAsync();
        public Task<List<Orders>> OrdersPage(int pageNumber, int pageSize, string searchString);
        public decimal GetOrderCost(Orders order);
    }
}
