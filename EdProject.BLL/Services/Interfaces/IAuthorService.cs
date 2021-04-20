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
        public Task<Author> GetAuthorById(long id);
        public Task<IEnumerable<Author>> GetAuthorList();
        public Task RemoveAuthorByIdAsync(long id);
    }
}
