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

        public async Task<List<Edition>> FilterEditionList(string searchString)
        {
            var editions = GetAll().Where(e => e.Id.ToString() == searchString ||
                                               e.Title == searchString ||
                                               e.Description.Contains(searchString)
                                               ); 
            
            return await editions.ToListAsync();
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
