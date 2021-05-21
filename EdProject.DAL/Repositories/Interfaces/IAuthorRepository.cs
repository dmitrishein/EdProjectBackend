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
        public Task AddEditionToAuthor(Author author, Edition edition);
        public Task AddEditionListToAuthor(Author author, List<Edition> editions);
        public Task RemoveAuthorInEdition(Author author, Edition edition);
        public Task RemoveAuthorInEditionList(Author author, List<Edition> editions);
    }
}
