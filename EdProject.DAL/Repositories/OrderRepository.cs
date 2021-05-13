using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class OrderRepository : BaseRepository<Orders>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public bool OrderExist(Orders order)
        {
            return GetAll().Any(o => o.Id == order.Id && o.PaymentId == order.PaymentId && o.UserId == order.UserId);
                
        }
        public async Task AddPaymentToOrderAsync(Orders order, Payments payments)
        {
            order.Payment = payments;
            await UpdateAsync(order);
        }
        public async Task AddItemToOrderAsync(Orders order, Edition edition)
        {
            order.Editions.Add(edition);
            await UpdateAsync(order);
        }
        public async Task RemoveItemToOrderAsync(Orders order, Edition edition)
        {
            order.Editions.Remove(edition);
            await UpdateAsync(order);
        }
        public async Task RemoveOrderByIdAsync(long id)
        {
            var res = await _dbSet.FindAsync(id);
            res.IsRemoved = true;
            await UpdateAsync(res);
        }
        public async Task RemoveOrderByPaymentIdAsync(long paymentId)
        {
            var order = GetAll().Where(e => e.PaymentId == paymentId).FirstOrDefault();
            order.IsRemoved = true;
            await UpdateAsync(order);
        }

        public async Task<List<Orders>> GetAllOrdersAsync()
        {
            return await GetAll().Where(x => !x.IsRemoved).ToListAsync();
        }
        public async Task<List<Orders>> GetOrderByUserIdAsync(long userId)
        {
            List<Orders> ordersQuery =  await GetAll().Where(e => e.UserId == userId).ToListAsync();

            return ordersQuery;
        }


        public async Task<List<Orders>> OrdersPage(int pageNumber, int pageSize,string searchString)
        {
            var ordersQuery = GetAll().Where(o => o.Id.ToString() == searchString ||
                                                  o.UserId.ToString().Contains(searchString) ||
                                                  o.PaymentId.ToString().Equals(searchString) ||
                                                  o.Description.Contains(searchString))
                                       .Where(e => !e.IsRemoved);

            var ordersPage = ordersQuery.Skip((pageNumber - Constants.SKIP_ZERO_PAGE) * pageSize).Take(pageSize);

            return await ordersPage.ToListAsync();
        }

    }
}
