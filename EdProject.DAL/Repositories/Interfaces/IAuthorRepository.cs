using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task RemoveAuthorById(long id);
        public Task AddEditionListToAuthor(Author author, List<Edition> editions);
        public Task RemoveAuthorInEditionList(Author author, List<Edition> editions);
    }
}
