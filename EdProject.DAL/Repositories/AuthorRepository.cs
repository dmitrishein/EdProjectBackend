using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext dbContext): base(dbContext)
        {
        }
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await GetAll().Where(x => !x.IsRemoved).ToListAsync();
        }
        public bool AuthorIsExist(Author author)
        {
            return GetAll().Any(item => item.Name == author.Name);
        }
        public async Task RemoveAuthorById(long id)
        {
            var res = await _dbSet.FindAsync(id);
            res.IsRemoved = true;
            await UpdateAsync(res); 
        }
        public async Task AddEditionToAuthor(Author author,Edition edition)
        {
            author.Editions.Add(edition);
            await UpdateAsync(author);
        }
        public async Task AddEditionListToAuthor(Author author, List<Edition> editions)
        {
            editions.ForEach(a => a.Authors.Add(author));
            await UpdateAsync(author);
        }
        public async Task RemoveAuthorInEdition(Author author, Edition edition)
        {
            author.Editions.Remove(edition);
            await UpdateAsync(author);
        }
        public async Task RemoveAuthorInEditionList(Author author, List<Edition> editions)
        {
            editions.ForEach(a => a.Authors.Remove(author));
            await UpdateAsync(author);
        }
    }
}
