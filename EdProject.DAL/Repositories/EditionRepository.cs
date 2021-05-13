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
        public async Task<List<Edition>> Pagination(int pageNumber,int pageSize, string searchString)
        {
            var listResults = GetAll().Where(e => e.Authors.Any(a => a.Name.Contains(searchString)) || 
                                             e.Title.Contains(searchString) || 
                                             e.Id.ToString().Equals(searchString))
                                      .Where(e => !e.IsRemoved);
            

            var editionsPage = listResults.Skip((pageNumber - Constants.SKIP_ZERO_PAGE) * pageSize).Take(pageSize);
            return await editionsPage.ToListAsync(); 
        }
    }
}
