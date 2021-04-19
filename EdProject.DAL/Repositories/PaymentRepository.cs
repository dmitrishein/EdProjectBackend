using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class PaymentRepository : BaseRepository<Payments>, IPaymentRepository
    {
        private AppDbContext _dbContext;
        protected DbSet<Payments> _payment;
        public PaymentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _dbContext = appDbContext;
            _payment = appDbContext.Set<Payments>();
        }

        public async Task RemovePaymentByIdAsync(long id)
        {
            var res = await _payment.FindAsync(id);
            if (res != null)
            {
                res.IsRemoved = true;
                _dbContext.Entry(res).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task RemovePaymentByTransactionIdAsync(long transactId)
        {
            IQueryable<Payments> paymentsQuery = _dbContext.Payments;
            var editions = paymentsQuery.Where(e => e.TransactionId == transactId);

            var transaction = paymentsQuery.FirstOrDefault();

            if (transaction != null)
            {
                transaction.IsRemoved = true;
                _dbContext.Entry(transaction).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
