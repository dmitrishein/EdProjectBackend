using EdProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IAuthorInPrintingEditionRepository : IBaseRepository<AuthorInEditions>
    {
        public IQueryable<AuthorInEditions> GetListByAuthorId(long authorId);
        public IQueryable<AuthorInEditions> GetListByEditionId(long editionId);
    }
}
