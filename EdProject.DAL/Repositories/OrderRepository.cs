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


        public async Task<List<Orders>> FilterOrderList(string searchString)
        {
            List<Orders> ordersQuery = await GetAll().Where(e => e.Id.ToString() == searchString)
                                                     .Where(e => e.PaymentId.ToString() == searchString)
                                                     .Where(e => e.Description.Contains(searchString)).ToListAsync();

            return ordersQuery;
        }
        public async Task<List<Orders>> PagingOrders(int pageNumber, int pageSize)
        {
            if (pageNumber == 0 || pageSize == 0)
            {
                return null;
            }

            var editionsPerPage = GetAll().Skip((pageNumber - Constant.SKIP_ZERO_PAGE) * pageSize).Take(pageSize);

            return await editionsPerPage.ToListAsync();
        }

    }
}
