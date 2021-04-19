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
        private AppDbContext _dbContext;
        protected DbSet<Edition> _edition;
        public PrintingEditionRepository(AppDbContext appDbContext) : base (appDbContext)
        {
            _dbContext = appDbContext;
            _edition = appDbContext.Set<Edition>();
        }
        

        public IQueryable<Edition> FilterEditionList(string searchString)
        {
            IQueryable<Edition> editionsQuery = _dbContext.Editions;
            var editions = editionsQuery.Where(e => e.Id.ToString() == searchString ||
                                               e.Title == searchString ||
                                               e.Description.Contains(searchString)
                                               );
            return editions;
        }
        public async Task RemoveEditionById(long id)
        {
            var res = await _edition.FindAsync(id);
            if (res != null)
            {
                res.IsRemoved = true;
                _dbContext.Entry(res).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
