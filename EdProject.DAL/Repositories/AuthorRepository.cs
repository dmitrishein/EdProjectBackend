using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        DbSet<Author> _author;
        AppDbContext _appDbContext;

        public AuthorRepository(AppDbContext dbContext): base(dbContext)
        {
            _appDbContext = dbContext;
            _author = dbContext.Set<Author>();
        }

        public async Task RemoveAuthorById(long id)
        {
            var res = await _author.FindAsync(id);
            if (res != null)
            {
                res.IsRemoved = true;
                _appDbContext.Entry(res).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
