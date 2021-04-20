using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class PaymentRepository : BaseRepository<Payments>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task RemovePaymentByIdAsync(long id)
        {
            var res = await _dbSet.FindAsync(id);

            if (res != null)
            {
                res.IsRemoved = true;
                await UpdateAsync(res);
                await SaveChangesAsync();
            }
        }
        public async Task RemovePaymentByTransactionIdAsync(string transactId)
        {
            IEnumerable<Payments> paymentsQuery = await GetAsync();
            var editions = paymentsQuery.Where(e => e.TransactionId == transactId);

            var transaction = paymentsQuery.FirstOrDefault();

            if (transaction != null)
            {
                transaction.IsRemoved = true;
                await UpdateAsync(transaction);
                await SaveChangesAsync();
            }

        }
    }
}
