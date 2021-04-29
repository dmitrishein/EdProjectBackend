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
        public OrderItemRepository(AppDbContext appDbContext) : base (appDbContext)
        {
        }

        public async Task RemoveOrderItemAsync(long id)
        {
            var res = await _dbSet.FindAsync(id);

            if (res is null)
                throw new Exception("Item wasn't found");

            res.IsRemoved = true;
            await UpdateAsync(res);
            
        }
    }
}
