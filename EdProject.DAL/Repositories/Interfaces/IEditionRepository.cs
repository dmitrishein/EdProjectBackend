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
        public Task RemoveEditionById(long id);
        public Task<List<Edition>> GetAllEditionsAsync();
        public Task<List<Edition>> Pagination(int pageNumber, int pageSize, string searchString);
        public Task AddEditionListToAuthor(Author author, string[] editionsId);
        public Task RemoveEditionListFromAuthor(Author author, string[] editionsId);
        public Task<List<Edition>> GetAllEditionsInOrderAsync(long orderId);
    }
}
