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
    public class AuthorInPrintingEditionRepository : BaseRepository<AuthorInEditions>, IAuthorInPrintingEditionRepository
    {

        
        public AuthorInPrintingEditionRepository(AppDbContext appDbContext) : base (appDbContext)
        {
          
        }

        public async Task<IEnumerable<AuthorInEditions>> GetListByAuthorId(long authorId)
        {
            IEnumerable<AuthorInEditions> editionsQuery = await GetAsync();
            var editions = editionsQuery.Where(e => e.AuthorId == authorId);
            return editions;
        }
        public async Task<IEnumerable<AuthorInEditions>> GetListByEditionId(long editionId)
        {
            IEnumerable<AuthorInEditions> editionsQuery = await GetAsync();
            var editions = editionsQuery.Where(e => e.EditionId == editionId);

            return editions;
        }

      
    }
}
