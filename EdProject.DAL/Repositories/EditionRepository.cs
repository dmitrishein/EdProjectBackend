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
    public class EditionRepository : BaseRepository<Edition>, IPrintingEditionRepository
    {
        public EditionRepository(AppDbContext appDbContext) : base (appDbContext)
        {
           
        }

        public bool IsExist(Edition edition)
        {
            return GetAll().Where(item => item.Title == edition.Title).Any();
        }
        public async Task RemoveEditionById(long id)
        {
            var res = await _dbSet.FindAsync(id);
            res.IsRemoved = true;
            await UpdateAsync(res);
        }
        public async Task<List<Edition>> GetAllEditionsAsync()
        {
            return await GetAll().Where(x =>!x.IsRemoved).ToListAsync();
        }

        public async Task<List<Edition>> Pagination(int pageNumber,int pageSize)
        {
            
            if (pageNumber is Constant.EMPTY || pageSize is Constant.EMPTY)
                return null;

            var editionsPerPage = GetAll().Skip((pageNumber - Constant.SKIP_ZERO_PAGE) * pageSize).Take(pageSize);

            return await editionsPerPage.ToListAsync(); 
        }
    }
}
