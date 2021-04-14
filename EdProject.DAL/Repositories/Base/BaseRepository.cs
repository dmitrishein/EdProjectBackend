using EdProject.DAL.DataContext;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity:class
    {
        #region private members

        private AppDbContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        #endregion


        #region constructor
        public BaseRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
            _dbSet = appDbContext.Set<TEntity>();
        }
        #endregion


        public async Task CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(TEntity item)
        {
            _dbSet.Remove(item);
          await _dbContext.SaveChangesAsync();
        }
        public async Task <TEntity> FindByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }
        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }
        public  IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
        public async Task UpdateAsync(TEntity item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
             await _dbContext.SaveChangesAsync();
        }


      
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

    }
    
}
