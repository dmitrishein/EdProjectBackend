using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Orders>
    {
        public Task AddItemToOrderAsync(Orders order, Edition edition);
        public Task RemoveItemToOrderAsync(Orders order, Edition edition);
        public Task RemoveOrderByIdAsync(long id);
        public Task<List<Orders>> GetOrderByUserIdAsync(long userId);
        public Task<List<Orders>> OrdersPage(int pageNumber, int pageSize, string searchString);
    }
}
