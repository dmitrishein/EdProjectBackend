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
    public class OrderRepository : BaseRepository<Orders>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }


        public async Task RemoveOrderByIdAsync(long id)
        {
            var res = await _dbSet.FindAsync(id);

            if (res != null)
            {
                res.IsRemoved = true;
                await UpdateAsync(res);
                await SaveChangesAsync();
            }
        }
        public async Task RemoveOrderByPaymentIdAsync(long paymentId)
        {
            IEnumerable<Orders> ordersQuery = await GetAllAsync();
            var editions = ordersQuery.Where(e => e.PaymentId == paymentId);

            var transaction = ordersQuery.FirstOrDefault();

            if (transaction != null)
            {
                transaction.IsRemoved = true;
                await UpdateAsync(transaction);
                await SaveChangesAsync();
            }

        }
        public async Task<IEnumerable<Orders>> FilterOrderList(string searchString)
        {
            IEnumerable<Orders> ordersQuery = await GetAllAsync() ;
            var orders = ordersQuery.Where(e => e.Id.ToString() == searchString ||
                                               e.PaymentId.ToString() == searchString ||
                                               e.Date.ToString() == searchString ||
                                               e.Description.Contains(searchString)
                                               );
            return orders ;
        }
        public async Task<IEnumerable<Orders>> GetOrderByUserId(long userId)
        {
            IEnumerable<Orders> ordersQuery = await GetAllAsync();
            var orders = ordersQuery.Where(e => e.UserId == userId);

            return orders;
        }
        public async Task<IEnumerable<Orders>> PagingOrders(int pageNumber, int pageSize)
        {
            IEnumerable<Orders> ordersPerPage = await GetAllAsync();

            if (pageNumber >= 1 && pageSize >= 1)
            {
                return ordersPerPage.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            return ordersPerPage;
        }

    }
}
