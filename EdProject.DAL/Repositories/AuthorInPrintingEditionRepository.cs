using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class AuthorInPrintingEditionRepository : BaseRepository<AuthorInEditions>, IAuthorInPrintingEditionRepository
    {

        private AppDbContext _dbContext;
        protected DbSet<AuthorInEditions> _authInEdition;
        public AuthorInPrintingEditionRepository(AppDbContext appDbContext) : base (appDbContext)
        {
            _dbContext = appDbContext;
            _authInEdition = appDbContext.Set<AuthorInEditions>();

        }

        public IQueryable<AuthorInEditions> GetListByAuthorId(long authorId)
        {
            IQueryable<AuthorInEditions> editionsQuery = _dbContext.AuthorInEditions;
            var editions = editionsQuery.Where(e => e.AuthorId == authorId);
            return editions;
        }
        public IQueryable<AuthorInEditions> GetListByEditionId(long editionId)
        {
            IQueryable<AuthorInEditions> editionsQuery = _dbContext.AuthorInEditions;
            var editions = editionsQuery.Where(e => e.EditionId == editionId);

            return editions;
        }
    }
}
