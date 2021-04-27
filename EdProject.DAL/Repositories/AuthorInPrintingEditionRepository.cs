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

        public async Task<List<AuthorInEditions>> GetListByAuthorId(long authorId)
        {
            var editions = GetAll().Where(e => e.AuthorId == authorId);

            return await editions.ToListAsync();
        }
        public async Task<List<AuthorInEditions>> GetListByEditionId(long editionId)
        {
            var editions = GetAll().Where(e => e.EditionId == editionId);

            return await editions.ToListAsync();
        }

      
    }
}
