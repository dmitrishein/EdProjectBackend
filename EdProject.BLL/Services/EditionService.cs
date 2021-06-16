using AutoMapper;
using EdProject.BLL.Models.Editions;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Models;
using EdProject.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        public async Task CreateEditionAsync(EditionModel editionModel)
        {
            var edition = _editionRepos.FindEditionByTitle(editionModel.Title);
            if (edition is not null)
            {
                throw new CustomException(ErrorConstant.ALREADY_EXIST, HttpStatusCode.BadRequest);
            }

            var newEdition = _mapper.Map<Edition>(editionModel);
            await _editionRepos.CreateAsync(newEdition);
        }
        public async Task UpdateEditionAsync(EditionModel editionModel)
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

        public async Task<List<EditionModel>> GetEditionsAsync()
        {
            var editionList = await _editionRepos.GetAllEditionsAsync();
            var result= _mapper.Map<List<EditionModel>>(editionList);
            return result;
        }
        public async Task<EditionModel> GetEditionByIdAsync(long id)
        {
            var getEdition = await _editionRepos.FindByIdAsync(id);

            if (getEdition is null || getEdition.IsRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);
            }

            return _mapper.Map<EditionModel>(getEdition);
        }
        public async Task<EditionPageResponseModel> GetEditionPageAsync(EditionPageParameters pageModel)
        {  
            var editionList = await _editionRepos.Pagination(pageModel);
            var editionPage = editionList.Skip((pageModel.PageNumber - VariableConstant.SKIP_ZERO_PAGE) * pageModel.ElementsAmount).Take(pageModel.ElementsAmount);

            EditionPageResponseModel editionPageResponse = new EditionPageResponseModel()
            {
                TotalPagesAmount = (editionList.Count + pageModel.ElementsAmount - VariableConstant.SKIP_INCORRECT_ROUNDING)/pageModel.ElementsAmount,
                Editions = _mapper.Map<List<EditionModel>>(editionPage),
            };
            if (!editionList.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.NoContent);
            }

            return editionPageResponse;
        }
    }
}
