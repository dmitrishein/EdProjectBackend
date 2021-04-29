﻿using EdProject.DAL.DataContext;
using EdProject.DAL.Entities.Base;
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

        public async Task<TEntity> FindByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity item)
        {
            _dbSet.Remove(item);
          await _dbContext.SaveChangesAsync();
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public async Task UpdateAsync(TEntity oldItem,TEntity newItem)
        {

            _dbContext.Entry(oldItem).CurrentValues.SetValues(newItem);
            
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
 
    }
    
}
