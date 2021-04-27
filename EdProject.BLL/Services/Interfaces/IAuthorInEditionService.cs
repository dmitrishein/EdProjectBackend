using EdProject.BLL.Models.Author;
using EdProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAuthorInEditionService
    {
        public Task CreateAuthInEdAsync(AuthorInEditionModel authInEditModel);
        public Task DeleteAuthInEditionAsync(AuthorInEditionModel authInEditModel);
        public Task UpdateAuthInEditAsync(AuthorInEditionModel authInEditModel);
        public Task<List<AuthorInEditions>> GetEditionsByAuthorId(long id);
        public Task<List<AuthorInEditions>> GetEditionsByEditionId(long id);
        public Task<List<AuthorInEditions>> GetList();
    }
}
