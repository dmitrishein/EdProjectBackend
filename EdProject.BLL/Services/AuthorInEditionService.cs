using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
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

        public IQueryable<AuthorInEditions> GetEditionsByAuthorId(long id)
        {
            return _authorInEditions.GetListByAuthorId(id);
        }

        public IQueryable<AuthorInEditions> GetEditionsByEditionId(long id)
        {
            return _authorInEditions.GetListByEditionId(id);
        }

        public IEnumerable<AuthorInEditions> GetList()
        {
            return _authorInEditions.Get();
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
