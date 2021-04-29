using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task CreateAsync(TEntity item);
        public Task<TEntity> FindByIdAsync(long id);
        public IQueryable<TEntity> GetAll();
        public Task UpdateAsync(TEntity old,TEntity item);
        public Task DeleteAsync(TEntity item);
    }
}
