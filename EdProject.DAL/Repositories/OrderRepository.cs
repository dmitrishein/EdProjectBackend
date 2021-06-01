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

        public async Task<List<Orders>> GetAllOrdersAsync()
        {
            return await GetAll().Where(x => !x.IsRemoved).ToListAsync();
        }
        public decimal GetOrderCost(Orders order)
        {
            decimal amount = order.OrderItems.Sum(x => x.Edition.Price * x.ItemsCount);
            return amount; 
        }

        public async Task<List<Orders>> GetOrdersByUserIdAsync(long userId)
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

            var ordersPage = ordersQuery.Skip((pageNumber - VariableConstant.SKIP_ZERO_PAGE) * pageSize).Take(pageSize);

            return await ordersPage.ToListAsync();
        }

    }
}
