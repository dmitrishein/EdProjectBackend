using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class PrintingEditionRepository : BaseRepository<Edition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(AppDbContext appDbContext) : base (appDbContext)
        {
           
        }

        public async Task<IEnumerable<Edition>> FilterEditionList(string searchString)
        {
            IEnumerable<Edition> editionsQuery = await GetAllAsync(); 
            var editions = editionsQuery.Where(e => e.Id.ToString() == searchString ||
                                               e.Title == searchString ||
                                               e.Description.Contains(searchString)
                                               );
            return editions;
        }
        public async Task RemoveEditionById(long id)
        {
            var res = await _dbSet.FindAsync(id);
            if (res != null)
            {
                res.IsRemoved = true;
                _dbContext.Entry(res).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Edition>> Paging(int pageNumber,int pageSize)
        {
            IEnumerable<Edition> editionsPerPage = await GetAllAsync();

            if (pageNumber >= 1 && pageSize>=1)
            {
                return editionsPerPage.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            return editionsPerPage;
        }
    }
}
