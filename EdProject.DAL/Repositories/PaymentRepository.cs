using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using System;
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

        public async Task RemovePaymentAsync(long id)
        {
            var res = await _dbSet.FindAsync(id);

            if (res is null)
            {
                throw new System.Exception("Payment wasn't found");
            }

            res.IsRemoved = true;
            await UpdateAsync(res);   
        }
        public async Task RemovePaymentByTransactionIdAsync(string transactId)
        {
            IQueryable<Payments> paymentsQuery = GetAll().Where(e => e.TransactionId == transactId);
           
            var transaction = paymentsQuery.FirstOrDefault();

            if (transaction is null)
                throw new Exception("Transaction is incorrect");
            
            transaction.IsRemoved = true;
             await UpdateAsync(transaction);
            
        }
    }
}
