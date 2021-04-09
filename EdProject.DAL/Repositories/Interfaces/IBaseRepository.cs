using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task Create(TEntity item);
        public Task<TEntity> FindById(long id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> GetAll(Func<TEntity,bool> predicate);
        public Task Update(TEntity item);
        public Task Remove(TEntity item);
    }
}
