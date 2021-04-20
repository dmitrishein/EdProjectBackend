using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Orders>
    {
        public Task RemoveOrderByIdAsync(long id);
        public Task RemoveOrderByPaymentIdAsync(long paymentId);
        public Task<IEnumerable<Orders>> FilterOrderList(string searchString);
        public Task<IEnumerable<Orders>> GetOrderByUserId(long userId);
        public Task<IEnumerable<Orders>> PagingOrders(int pageNumber, int pageSize);
    }
}
