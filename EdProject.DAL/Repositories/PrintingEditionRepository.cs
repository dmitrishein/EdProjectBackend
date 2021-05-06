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
    public class PrintingEditionRepository : BaseRepository<Edition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(AppDbContext appDbContext) : base (appDbContext)
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
            const int skipZeroPage = 1;
            if (pageNumber == 0 || pageSize == 0)
                return null;

            var editionsPerPage = GetAll().Skip((pageNumber - skipZeroPage) * pageSize).Take(pageSize);
            return await editionsPerPage.ToListAsync(); 
        }
    }
}
