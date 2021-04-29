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

            if (res is not null)
            {
                res.IsRemoved = true;
                await UpdateAsync(res);
            }
        }
        public async Task RemoveOrderByPaymentIdAsync(long paymentId)
        {
            IQueryable<Orders> ordersQuery =  GetAll().Where(e => e.PaymentId == paymentId);

            var order = ordersQuery.FirstOrDefault();

            if (order is not null)
            {
                order.IsRemoved = true;
                await UpdateAsync(order);
            }

        }
        public async Task<List<Orders>> FilterOrderList(string searchString)
        {
            IQueryable<Orders> ordersQuery =  GetAll().Where(e => e.Id.ToString() == searchString ||
                                               e.PaymentId.ToString() == searchString ||
                                               e.Date.ToString() == searchString ||
                                               e.Description.Contains(searchString)
                                               );
            
            return await ordersQuery.ToListAsync();
        }
        public async Task<List<Orders>> GetOrderByUserId(long userId)
        {
            IQueryable<Orders> ordersQuery =  GetAll().Where(e => e.UserId == userId);

            return await ordersQuery.ToListAsync();
        }
        public async Task<List<Orders>> PagingOrders(int pageNumber, int pageSize)
        {
            const int skipZeroPage = 1;
            if (pageNumber == 0 || pageSize == 0)
                return null;

            var editionsPerPage = GetAll().Skip((pageNumber - skipZeroPage) * pageSize).Take(pageSize);

            return await editionsPerPage.ToListAsync();
        }

    }
}
