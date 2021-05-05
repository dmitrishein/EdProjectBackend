using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        AuthorRepository _authorRepositroy;
        IMapper _mapper;
        public AuthorService(AppDbContext appDb,IMapper mapper)
        {
            _authorRepositroy = new AuthorRepository(appDb);
            _mapper = mapper;
        }

        public async Task CreateAuthorAsync(AuthorModel authorModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorModel, Author>());
            var _mapper = new Mapper(config);
            var newAuthor = _mapper.Map<AuthorModel, Author>(authorModel);

            await _authorRepositroy.CreateAsync(newAuthor);
        }
        public async Task<AuthorModel> GetAuthorById(long id)
        {
            try
            {
                var authorIn = await _authorRepositroy.FindByIdAsync(id);
                
                return _mapper.Map<Author, AuthorModel>(authorIn);          
            }
            catch(Exception x)
            {
                throw new Exception($"Author wasn't found. {x.Message}");
            }
 
        }
        public List<AuthorModel> GetAuthorList()
        {
            if (!_authorRepositroy.GetAllAuthors().Any())
                throw new Exception("Author wasn't found");

            return _mapper.Map<List<Author>, List<AuthorModel>>(_authorRepositroy.GetAllAuthors());          
        }
        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {
          
            var updatedAuthor = _mapper.Map<AuthorModel, Author>(authorModel);
            var oldAuthor = await _authorRepositroy.FindByIdAsync(updatedAuthor.Id);

            if (oldAuthor is null)
                throw new Exception("Error! Author wasn't found");

            if (oldAuthor.IsRemoved is true)
                throw new Exception("Cannot update. Author was removed");

            await _authorRepositroy.UpdateAsync(oldAuthor,updatedAuthor);
        }
        public async Task RemoveAuthorAsync(long id)
        {
            try
            {
                await _authorRepositroy.RemoveAuthorById(id);
            }
            catch(Exception x)
            {
                throw new Exception($"Error! Cannot remove Author { x.Message}");
            }
        }

    }
}
