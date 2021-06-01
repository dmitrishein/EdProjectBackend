using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task UpdateAuthorAsync(AuthorModel authorModel);
        public Task CreateAuthorAsync(AuthorModel authorModel);
        public Task AddAuthorToEditionAsync(AuthorModel authorModel);
        public Task<AuthorModel> GetAuthorByIdAsync(long id);
        public Task<List<AuthorModel>> GetAuthorsAsync();
        public Task RemoveAuthorAsync(long id);
        public Task<List<EditionModel>> GetEditionsByAuthorIdAsync(long authorId);
        public Task<List<AuthorModel>> GetAuthorsByEditionIdAsync(long editionId);
    }
}
