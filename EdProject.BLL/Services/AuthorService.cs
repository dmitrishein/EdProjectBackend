using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using EdProject.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        AuthorRepository _authorRepositroy;

        public AuthorService(AppDbContext appDb)
        {
            _authorRepositroy = new AuthorRepository(appDb);
        }

        public async Task CreateAuthorAsync(AuthorModel authorModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorModel, Author>());
            var _mapper = new Mapper(config);
            var newAuthor = _mapper.Map<AuthorModel, Author>(authorModel);

            await _authorRepositroy.CreateAsync(newAuthor);
        }
        public async Task<Author> GetAuthorById(long id)
        {
            return await _authorRepositroy.FindByIdAsync(id);
        }
        public Task<IEnumerable<Author>> GetAuthorList()
        {
            return _authorRepositroy.GetAsync();
        }
        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorModel, Author>());
            var _mapper = new Mapper(config);
            var updateUser = _mapper.Map<AuthorModel, Author>(authorModel);

            await _authorRepositroy.UpdateAsync(updateUser);
        }
        public async Task RemoveAuthorByIdAsync(long id)
        {
            await _authorRepositroy.RemoveAuthorById(id);
        }

    }
}
