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

        public Author FindAuthorByName(string authorName)
        {
            return GetAll().FirstOrDefault(a => a.Name == authorName && !a.IsRemoved);
        }
        public async Task RemoveAuthorById(long id)
        {
            var res = await _dbSet.FindAsync(id);
            res.IsRemoved = true;
            await UpdateAsync(res); 
        }

    }
}
