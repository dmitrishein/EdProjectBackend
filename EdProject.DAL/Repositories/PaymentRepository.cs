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

            if (res is not null)
            {
                res.IsRemoved = true;
                await UpdateAsync(res);
            }
        }
        public async Task RemovePaymentByTransactionIdAsync(string transactId)
        {
            IQueryable<Payments> paymentsQuery =  GetAll().Where(e => e.TransactionId == transactId);
           
            var transaction = paymentsQuery.FirstOrDefault();

            if (transaction is not null)
            {
                transaction.IsRemoved = true;
                await UpdateAsync(transaction);
            }
        }
    }
}
