using AutoMapper;
using EdProject.BLL.Models.Editions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Models;
using EdProject.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using EdProject.BLL.Models.ViewModels;

namespace EdProject.BLL.Services
{
    public class EditionService : IEditionService
    {
        
        IEditionRepository _editionRepos;
        IMapper _mapper;
        public EditionService(IEditionRepository editionRepository,IMapper mapper)
        {
            _editionRepos = editionRepository;
            _mapper = mapper;
        }

        public async Task CreateEditionAsync(CreateEditionModel editionModel)
        {
            var edition = _editionRepos.FindEditionByTitle(editionModel.Title);
            if (edition is not null)
            {
                throw new CustomException(ErrorConstant.ALREADY_EXIST, HttpStatusCode.BadRequest);
            }

            var newEdition = _mapper.Map<Edition>(editionModel);
            await _editionRepos.CreateAsync(newEdition);
        }
        public async Task UpdateEditionAsync(CreateEditionModel editionModel)
        {
            var newEdition = _mapper.Map<Edition>(editionModel);
            var oldEdition = await _editionRepos.FindByIdAsync(newEdition.Id);

            if (oldEdition is null || oldEdition.IsRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);
            }

            await _editionRepos.UpdateAsync(oldEdition,newEdition);
        }

        public async Task RemoveEditionAsync(long id)
        {
            var editionToRemove= await _editionRepos.FindByIdAsync(id);
            if (editionToRemove is null || editionToRemove.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_EDITION, HttpStatusCode.BadRequest);
            }    
            editionToRemove.IsRemoved = true;
            await _editionRepos.UpdateAsync(editionToRemove);
        }
        public async Task<CreateEditionModel> GetEditionByIdAsync(long id)
        {
            var getEdition = await _editionRepos.FindByIdAsync(id);

            if (getEdition is null || getEdition.IsRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);
            }

            return _mapper.Map<CreateEditionModel>(getEdition);
        }
        public async Task<ProductsViewModel> GetViewModelAsync(EditionPageParameters pageParams)
        {
            var pageResponseModel = await GetEditionPageAsync(pageParams);
            ProductsViewModel authorsViewModel = new ProductsViewModel
            {
                ElementsPerPage = pageParams.ElementsPerPage,
                CurrentPage = pageResponseModel.CurrentPage,
                TotalItemsAmount = pageResponseModel.TotalItemsAmount,
                EditionsPage = pageResponseModel.EditionsPage
            };
            return authorsViewModel;
        }
        public async Task<EditionPageResponseModel> GetEditionPageAsync(EditionPageParameters pageParams)
        {  
            var editionPageModel = await _editionRepos.Pagination(pageParams);
            var response = _mapper.Map<EditionPageResponseModel>(editionPageModel);
            return response;
        }
    }
}
