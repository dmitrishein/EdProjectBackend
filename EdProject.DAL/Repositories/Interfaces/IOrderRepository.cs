using EdProject.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Orders>
    {
        public Task RemoveOrderByIdAsync(long id);
        public  Task RemoveOrderByPaymentIdAsync(long paymentId);
        public IQueryable<Orders> FilterOrderList(string searchString);
    }
}
