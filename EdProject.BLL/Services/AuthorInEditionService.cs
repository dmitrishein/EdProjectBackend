using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AuthorInEditionService : IAuthorInEditionService
    {
        AuthorInPrintingEditionRepository _authorInEditions;
        IMapper _mapper;

        public AuthorInEditionService(AppDbContext appDbContext,IMapper mapper)
        {
            _authorInEditions = new AuthorInPrintingEditionRepository(appDbContext);
            _mapper = mapper;
        }

        public async Task CreateAuthInEdAsync(AuthorInEditionModel authInEditModel)
        {
            var newAuthorInEdit = _mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel);
            if (_authorInEditions.AuthorInEdtionExist(newAuthorInEdit))
                throw new CustomException("Author already exist in this edition!",400);
            await _authorInEditions.CreateAsync(newAuthorInEdit);
        }
        public async Task DeleteAuthInEditionAsync(AuthorInEditionModel authInEditModel)
        {   
            await _authorInEditions.DeleteAsync(_mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel));
        }
        public async Task<List<AuthorInEditionModel>> GetEditionsByAuthorId(long authorId)
        {
            List<AuthorInEditions> queryList = await _authorInEditions.GetEditionsByAuthorAsync(authorId);

            if (!queryList.Any())
                throw new CustomException("This author hasn't edition's",200);

            return _mapper.Map<List<AuthorInEditions>, List<AuthorInEditionModel>>(queryList);
        }
        public async Task<List<AuthorInEditionModel>> GetAuthorsByEditionId(long editionId)
        {

            List<AuthorInEditions> queryList = await _authorInEditions.GetAuthorsByEditionAsync(editionId);

            if (!queryList.Any())
                throw new CustomException("Edition was not found!",200);

            return _mapper.Map<List<AuthorInEditions>, List<AuthorInEditionModel>>(queryList);

        }
        public async Task<List<AuthorInEditionModel>> GetList()
        {
            List<AuthorInEditions> queryList = await _authorInEditions.GetAllAuthorInEditionAsync();

            if (!queryList.Any())
                throw new CustomException("List is empty!",200);

            return _mapper.Map<List<AuthorInEditions>, List<AuthorInEditionModel>>(queryList);   
        }
        public async Task UpdateAuthorInEditAsync(AuthorInEditionModel authInEditModel)
        {
            var oldItem =(await _authorInEditions.GetAllAuthorInEditionAsync()).Where(x => x.EditionId == authInEditModel.EditionId).FirstOrDefault();
            var newItem = _mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel);

            await _authorInEditions.UpdateAsync(oldItem,newItem);

        }
    }
}
