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
        public async Task<AuthorModel> GetAuthorById(long id)
        {
            try
            {
                var authorIn = await _authorRepositroy.FindByIdAsync(id);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorModel>());
                var _mapper = new Mapper(config);
                var authorOut = _mapper.Map<Author, AuthorModel>(authorIn);

                return authorOut;
            }
            catch(Exception x)
            {
                throw new Exception($"Author wasn't found. {x.Message}");
            }
 
        }
        public List<AuthorModel> GetAuthorList()
        {
            List<Author> AuthorList = _authorRepositroy.GetAll().Where(x => x.IsRemoved == false).ToList();
            List<AuthorModel> authorModels = new List<AuthorModel>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorModel>());
            var _mapper = new Mapper(config);

            foreach (Author author in AuthorList)
            {
                var authorModel = _mapper.Map<Author, AuthorModel>(author);
                authorModels.Add(authorModel);
            }
            return authorModels;
        }
        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorModel, Author>());
            var _mapper = new Mapper(config);
            var updateAuthor = _mapper.Map<AuthorModel, Author>(authorModel);

            var oldAuthor = await _authorRepositroy.FindByIdAsync(updateAuthor.Id);

            if (oldAuthor is null)
                throw new Exception("Error! Author wasn't found");

            if (oldAuthor.IsRemoved is true)
                throw new Exception("Cannot update. Author was removed");

            await _authorRepositroy.UpdateAsync(oldAuthor,updateAuthor);
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
