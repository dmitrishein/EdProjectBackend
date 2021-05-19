using EdProject.DAL.Entities;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payments>
    {
        public Task RemovePaymentAsync(long id);
    }
}
