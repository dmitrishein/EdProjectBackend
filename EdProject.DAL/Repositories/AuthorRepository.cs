using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
       
        public AuthorRepository(AppDbContext dbContext): base(dbContext)
        {
        }

        public async Task RemoveAuthorById(long id)
        {
            var res = await _dbSet.FindAsync(id);
            if (res is null)
                throw new System.Exception("Author wasn't found in database");

             res.IsRemoved = true;
             await UpdateAsync(res); 
        }
    }
}
