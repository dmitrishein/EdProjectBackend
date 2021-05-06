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
        Task<List<AuthorInEditions>> GetListByAuthorIdAsync(long authorId);
        Task<List<AuthorInEditions>> GetListByEditionIdAsync(long editionId);
        Task<List<AuthorInEditions>> GetEditionsByAuthorAsync(long authorId);
        Task<List<AuthorInEditions>> GetAuthorsByEditionAsync(long editionId);
        bool AuthorInEdtionExist(AuthorInEditions author);
    }
}
