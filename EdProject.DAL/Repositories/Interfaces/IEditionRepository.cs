using EdProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IEditionRepository : IBaseRepository<Edition>
    {
        public Edition FindEditionByTitle(string title);
        public Task<List<Edition>> GetEditionRangeAsync(List<long> editionsId);
        public Task<List<Edition>> GetAllEditionsAsync();
        public Task<List<Edition>> Pagination(int pageNumber, int pageSize, string searchString);
        public Task<List<Edition>> GetAllEditionsInOrderAsync(long orderId);
        public Task<List<Edition>> GetAllAuthorEditionsAsync(long authorId);
    }
}
