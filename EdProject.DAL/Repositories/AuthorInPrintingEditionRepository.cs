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

        public bool AuthorInEdtionExist(AuthorInEditions author)
        {
            return GetAll().Where(item => item.AuthorId == author.AuthorId && item.EditionId == author.EditionId).Any();
        }
        public async Task<List<AuthorInEditions>> GetAllAuthorInEditionAsync()
        {
            return await GetAll().ToListAsync();
        }
        public async Task<List<AuthorInEditions>> GetAuthorsByEditionAsync(long editionId)
        {
           return await GetAll().Where(x => x.EditionId == editionId).ToListAsync();
        }
        public async Task<List<AuthorInEditions>> GetEditionsByAuthorAsync(long authorId)
        {
            return await GetAll().Where(x => x.AuthorId == authorId).ToListAsync();
        }
        public async Task<List<AuthorInEditions>> GetListByAuthorIdAsync(long authorId)
        {

            return await GetAll().Where(e => e.AuthorId == authorId).ToListAsync();
        }
        public async Task<List<AuthorInEditions>> GetListByEditionIdAsync(long editionId)
        {

            return await GetAll().Where(e => e.EditionId == editionId).ToListAsync();
        }

      
    }
}
