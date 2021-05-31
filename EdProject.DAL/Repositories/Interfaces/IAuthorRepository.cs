using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task<List<Author>> GetAllAuthorsAsync();
        public Author FindAuthorByName(string authorName);
        public Task RemoveAuthorById(long id);
    }
}
