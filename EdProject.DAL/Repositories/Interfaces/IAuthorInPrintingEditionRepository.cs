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
        Task<IEnumerable<AuthorInEditions>> GetListByAuthorId(long authorId);
        Task<IEnumerable<AuthorInEditions>> GetListByEditionId(long editionId);
    }
}
