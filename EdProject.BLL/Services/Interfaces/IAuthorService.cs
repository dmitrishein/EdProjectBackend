using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.AuthorDTO;
using EdProject.BLL.Models.Editions;
using EdProject.BLL.Models.ViewModels;
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
        public Task<AuthorsViewModel> GetAuthorsViewModel();
        public Task UpdateAuthorAsync(AuthorModel authorModel);
        public Task CreateAuthorAsync(AuthorModel authorModel);
        public Task AddAuthorToEditionAsync(AddAuthorToEdition authorModel);
        public Task<AuthorModel> GetAuthorByIdAsync(long id);
        public Task RemoveAuthorAsync(long id);
    }
}
