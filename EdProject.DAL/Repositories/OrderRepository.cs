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
        private AppDbContext _dbContext;
        protected DbSet<Orders> _order;
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _dbContext = appDbContext;
            _order = appDbContext.Set<Orders>();
        }


        public async Task RemoveOrderByIdAsync(long id)
        {
            var res = await _order.FindAsync(id);

            if (res != null)
            {
                res.IsRemoved = true;
                _dbContext.Entry(res).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task RemoveOrderByPaymentIdAsync(long paymentId)
        {
            IQueryable<Orders> ordersQuery = _dbContext.Orders;
            var editions = ordersQuery.Where(e => e.PaymentId == paymentId);

            var transaction = ordersQuery.FirstOrDefault();

            if (transaction != null)
            {
                transaction.IsRemoved = true;
                _dbContext.Entry(transaction).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

        }
        public IQueryable<Orders> FilterOrderList(string searchString)
        {
            IQueryable<Orders> ordersQuery = _dbContext.Orders;
            var orders = ordersQuery.Where(e => e.Id.ToString() == searchString ||
                                               e.PaymentId.ToString() == searchString ||
                                               e.Date.ToString() == searchString ||
                                               e.Description.Contains(searchString)
                                               );
            return orders ;
        }
        public IQueryable<Orders> GetOrderByUserId(long userId)
        {
            IQueryable<Orders> ordersQuery = _dbContext.Orders;
            var orders = ordersQuery.Where(e => e.UserId == userId);

            return orders;
        }


    }
}
