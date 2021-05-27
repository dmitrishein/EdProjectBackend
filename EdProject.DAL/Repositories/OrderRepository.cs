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
        public async Task AddItemToOrderAsync(Orders order, OrderItem item)
        {
            order.OrderItems.Add(item);
            await UpdateAsync(order);
        }
        public async Task AddItemListToOrderAsync(Orders order, List<OrderItem> items)
        {
            items.ForEach(item => order.OrderItems.Add(item));
            await UpdateAsync(order);
        }

        public async Task RemoveItemToOrderAsync(Orders order, OrderItem item)
        {
            order.OrderItems.Remove(item);
            await UpdateAsync(order);
        }
        public async Task RemoveItemListFromOrderAsync(Orders order, List<OrderItem> items)
        {
            items.ForEach(item => order.OrderItems.Remove(item));
            await UpdateAsync(order);
        }
        public async Task RemoveOrderByIdAsync(long id)
        {
            var res = await _dbSet.FindAsync(id);
            res.IsRemoved = true;
            await UpdateAsync(res);
        }
        public async Task ClearOrderByIdAsync(Orders order)
        {
            order.OrderItems.Clear();
            await UpdateAsync(order);
        }

        public async Task UpdateOrderItem(Orders order,OrderItem orderItem)
        {
            var item = order.OrderItems.First(ed => ed.EditionId == orderItem.EditionId);
            item.ItemsCount = orderItem.ItemsCount;
            item.Amount = item.Edition.Price * orderItem.ItemsCount;
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

        public bool OrderItemIsExist(Orders order, OrderItem item)
        {
            var orderItem = order.OrderItems.First(orderItem => orderItem.EditionId == item.EditionId);
            if(orderItem is not null)
            {
                return true;
            }
            return false;
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
