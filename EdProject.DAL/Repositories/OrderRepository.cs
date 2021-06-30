using System.Linq.Dynamic.Core;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Pagination.Models;
using EdProject.DAL.Pagination.Models.Order;
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
        public async Task<OrderPageModel> OrdersPage(OrdersPageParameters pageParameters)
        {
            var countItems = await GetAll()
                                     .Where(o => o.Description.Contains(pageParameters.SearchString) ||
                                                 o.Payment.Amount.Equals(pageParameters.SearchString) ||
                                                 o.Id.ToString().Contains(pageParameters.SearchString) ||
                                                 o.Editions.Any(e => e.Title.Contains(pageParameters.SearchString)) ||
                                                 o.Editions.Any(e => e.Authors.Any(o => o.Name.Contains(pageParameters.SearchString))))
                                     .Where(o => o.UserId.ToString().Equals(pageParameters.UserId))
                                     .Where(o => !o.IsRemoved)
                                     .CountAsync();

            var orderToResult = GetAll().Where(o => o.Description.Contains(pageParameters.SearchString) ||
                                     o.Payment.Amount.Equals(pageParameters.SearchString) ||
                                     o.Id.ToString().Contains(pageParameters.SearchString) ||
                                     o.Editions.Any(e => e.Title.Contains(pageParameters.SearchString)) ||
                                     o.Editions.Any(e => e.Authors.Any(o => o.Name.Contains(pageParameters.SearchString))))
                         .Where(o => o.UserId.ToString().Equals(pageParameters.UserId))
                         .Where(o => !o.IsRemoved)
                         .OrderBy(pageParameters.SortType == 0 ? "Id" : $"{pageParameters.SortType.ToString()} {(pageParameters.IsReversed ? "DESC":"ASC")}" )
                         .Skip((pageParameters.CurrentPageNumber - VariableConstant.SKIP_ZERO_PAGE) * pageParameters.ElementsPerPage)
                         .Take(pageParameters.ElementsPerPage);

            OrderPageModel page = new OrderPageModel
            {
                TotalItemsAmount = countItems,
                CurrentPage = pageParameters.CurrentPageNumber,
                OrdersPage = await orderToResult.ToListAsync()
            };
            return page;
        }

    }
}
