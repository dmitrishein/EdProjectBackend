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
        Task<List<AuthorInEditions>> GetListByAuthorId(long authorId);
        Task<List<AuthorInEditions>> GetListByEditionId(long editionId);
    }
}
