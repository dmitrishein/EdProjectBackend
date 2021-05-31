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
    public class EditionRepository : BaseRepository<Edition>, IEditionRepository
    {
        public EditionRepository(AppDbContext appDbContext) : base (appDbContext)
        {
           
        }
 
        public Edition FindEditionByTitle(string title)
        {
            return GetAll().FirstOrDefault(e => e.Title == title && !e.IsRemoved);
        }  
        public async Task<List<Edition>> GetEditionRangeAsync(List<long> editionsId)
        {
            var editionList = GetAll().Where(ed => editionsId.Contains(ed.Id));

            return await editionList.ToListAsync();
        }
        public async Task<List<Edition>> GetAllEditionsAsync()
        {
            return await GetAll().Where(x =>!x.IsRemoved).ToListAsync();
        }
        public async Task<List<Edition>> GetAllAuthorEditionsAsync(long authorId)
        {
            return await GetAll().Where(x => !x.IsRemoved && x.Authors.Any(y => y.Id == authorId)).ToListAsync();
        }
        public async Task<List<Edition>> GetAllEditionsInOrderAsync(long orderId)
        {
            return await GetAll().Where(x => x.Orders.Any(y=> y.Id == orderId)).ToListAsync();
        }
        public async Task<List<Edition>> Pagination(int pageNumber,int pageSize, string searchString)
        {
            var listResults = GetAll().Where(e => e.Authors.Any(a => a.Name.Contains(searchString)) || 
                                             e.Title.Contains(searchString) || 
                                             e.Id.ToString().Equals(searchString))
                                      .Where(e => !e.IsRemoved);
            

            var editionsPage = listResults.Skip((pageNumber - VariableConstant.SKIP_ZERO_PAGE) * pageSize).Take(pageSize);

            return await editionsPage.ToListAsync(); 
        }
    }
}
