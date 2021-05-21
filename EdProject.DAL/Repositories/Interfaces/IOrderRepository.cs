using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Orders>
    {
        public Task AddPaymentToOrderAsync(Orders order, Payments payments);
        public Task AddItemToOrderAsync(Orders order, Edition edition);
        public Task AddItemListToOrderAsync(Orders order, List<Edition> editions);
        public Task RemoveItemToOrderAsync(Orders order, Edition edition);
        public Task RemoveOrderByIdAsync(long id);
        public Task RemoveItemListFromOrderAsync(Orders order, List<Edition> editions);
        public Task<List<Orders>> GetOrderByUserIdAsync(long userId);
        public Task<List<Orders>> GetAllOrdersAsync();
        public Task<List<Orders>> OrdersPage(int pageNumber, int pageSize, string searchString);
    }
}
