using EdProject.BLL.Models.Author;
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
        public Task<AuthorModel> GetAuthorById(long id);
        public List<AuthorModel> GetAuthorList();
        public Task RemoveAuthorAsync(long id);
    }
}
