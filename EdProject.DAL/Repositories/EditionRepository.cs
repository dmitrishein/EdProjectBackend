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
        public async Task AddEditionListToAuthor(Author author, string[]editionsId)
        {
            var editionsInRepos = await GetAllEditionsAsync();

            foreach (var id in editionsId)
            {
                var item = editionsInRepos.Find(ed => ed.Id == int.Parse(id));
                if (item is null)
                {
                    continue;
                }
                item.Authors.Add(author);
            }

            await SaveChangesAsync();
        }
        public async Task RemoveEditionListFromAuthor(Author author, string[] editionsId)
        {
            var editionsInRepos = await GetAllEditionsAsync();

            foreach (var id in editionsId)
            {
                var item = editionsInRepos.Find(ed => ed.Id == int.Parse(id));
                if (item is null)
                {
                    continue;
                }
                item.Authors.Remove(author);
            }

            await SaveChangesAsync();
        }
        public Edition FindEditionByTitle(string title)
        {
            return GetAll().FirstOrDefault(e => e.Title == title);
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
