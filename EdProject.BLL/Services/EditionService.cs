using AutoMapper;
using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
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
            if (edition is not null && edition.IsRemoved)
            {
                await _editionRepos.DeleteAsync(edition);
            }
            if (edition is not null && !edition.IsRemoved)
            {
                throw new CustomException(ErrorConstant.ALREADY_EXIST, HttpStatusCode.BadRequest);
            }

            var newEdition = _mapper.Map<EditionModel, Edition>(editionModel);
            await _editionRepos.CreateAsync(newEdition);
        }
        public async Task UpdateEditionAsync(EditionModel editionModel)
        {
            var newEdition = _mapper.Map<EditionModel, Edition>(editionModel);
            var oldEdition = await _editionRepos.FindByIdAsync(newEdition.Id);

            if (oldEdition is null || newEdition.IsRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);
            }

            await _editionRepos.UpdateAsync(oldEdition,newEdition);

        }
        public async Task RemoveEditionAsync(long id)
        {
            await _editionRepos.RemoveEditionById(id);
        }

        public async Task<List<EditionModel>> GetEditionListAsync()
        {
            var editionList = await _editionRepos.GetAllEditionsAsync();

            if (!editionList.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.NoContent);
            }

            return _mapper.Map<List<Edition>, List<EditionModel>>(editionList);
        }
        public async Task<EditionModel> GetEditionByIdAsync(long id)
        {
            var getEdition = await _editionRepos.FindByIdAsync(id);
            if (getEdition is null || getEdition.IsRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);
            }

            return _mapper.Map<Edition, EditionModel>(getEdition);
        }
        public async Task<List<EditionModel>> GetEditionListByStringAsync(string searchString)
        {
            var editionList = (await _editionRepos.GetAllEditionsAsync()).Where(x => x.Id.ToString() == searchString)
                                                                   .Where(x => x.Title == searchString).ToList();


            if (!editionList.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.NoContent);
            }

            return _mapper.Map<List<Edition>, List<EditionModel>>(editionList);
        }
        public async Task<List<EditionModel>> GetEditionPageAsync(PageModel pageModel)
        {
            var editionList = await _editionRepos.Pagination(pageModel.PageNumber,pageModel.ElementsAmount,pageModel.SearchString);
            if (!editionList.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.NoContent);
            }

            return _mapper.Map<List<Edition>, List<EditionModel>>(editionList);
        }
    }
}
