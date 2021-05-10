using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Payments>> GetAllPayments()
        {
           return await GetAll().ToListAsync();
        }
        public async Task RemovePaymentAsync(long id)
        {
            var res = await _dbSet.FindAsync(id);
            res.IsRemoved = true;
            await UpdateAsync(res);   
        }
        public async Task RemovePaymentByTransactionIdAsync(string transactId)
        {    
            var transaction = GetAll().Where(e => e.TransactionId == transactId).FirstOrDefault();
            transaction.IsRemoved = true;
            await UpdateAsync(transaction);
        }
    }
}
