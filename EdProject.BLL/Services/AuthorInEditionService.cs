using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AuthorInEditionService : IAuthorInEditionService
    {
        AuthorInPrintingEditionRepository _authorInEditions;

        public AuthorInEditionService(AppDbContext appDbContext)
        {
            _authorInEditions = new AuthorInPrintingEditionRepository(appDbContext);
        }

        public async Task CreateAuthInEdAsync(AuthorInEditionModel authInEditModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditionModel, AuthorInEditions>());
            var _mapper = new Mapper(config);
            var newItem = _mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel);

            await _authorInEditions.CreateAsync(newItem);
        }

        public async Task DeleteAuthInEditionAsync(AuthorInEditionModel authInEditModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditionModel, AuthorInEditions>());
            var _mapper = new Mapper(config);
            var itemToRemove = _mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel);

            await _authorInEditions.DeleteAsync(itemToRemove);

        }
        public async Task<List<AuthorInEditions>> GetEditionsByAuthorId(long id)
        {
            return await _authorInEditions.GetListByAuthorId(id);
        }
        public async Task<List<AuthorInEditions>> GetEditionsByEditionId(long id)
        {
            return await _authorInEditions.GetListByEditionId(id);
        }
        public Task<List<AuthorInEditions>> GetList()
        {
            return _authorInEditions.GetAll().ToListAsync();
        }
        public async Task UpdateAuthInEditAsync(AuthorInEditionModel authInEditModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditionModel, AuthorInEditions>());
            var _mapper = new Mapper(config);
            var newModel = _mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel);

            await _authorInEditions.UpdateAsync(newModel);
        }
    }
}
