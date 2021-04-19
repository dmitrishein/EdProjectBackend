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
    public class OrderItemRepository : BaseRepository<OrderItems>, IOrderItemRepository
    {
        private AppDbContext _dbContext;
        protected DbSet<OrderItems> _orderItems;
        public OrderItemRepository(AppDbContext appDbContext) : base (appDbContext)
        {
            _dbContext = appDbContext;
            _orderItems = appDbContext.Set<OrderItems>();
        }

        public async Task RemoveOrderItemByIdAsync(long id)
        {
            var res = await _orderItems.FindAsync(id);
            if (res != null)
            {
                res.IsRemoved = true;
                _dbContext.Entry(res).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
