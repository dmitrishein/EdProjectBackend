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
        public async Task RemoveEditionById(long id)
        {
            var res = await _dbSet.FindAsync(id);
            if (res is null)
                throw new System.Exception("Edition wasn't found in database");

            res.IsRemoved = true;
            await UpdateAsync(res);
        }

        public List<Edition> GetAllEditions()
        {
            
            return base.GetAll().Where(x =>!x.IsRemoved).ToList();
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
