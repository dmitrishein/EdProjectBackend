using AutoMapper;
using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class EditionService : IPrintingEditionService
    {
        
        EditionRepository _editionRepos;
        IMapper _mapper;
        public EditionService(AppDbContext appDbContext,IMapper mapper)
        {
            _editionRepos = new EditionRepository(appDbContext);
            _mapper = mapper;
        }

        public async Task CreateEditionAsync(EditionModel editionModel)
        {
            EditionModelValidation(editionModel);
            if (_editionRepos.IsExist(_mapper.Map<EditionModel, Edition>(editionModel)))
                throw new CustomException("Error! Edition already exist!", HttpStatusCode.BadRequest);

            var newEdition = _mapper.Map<EditionModel, Edition>(editionModel);

            await _editionRepos.CreateAsync(newEdition);
        }
        public async Task UpdateEditionAsync(EditionModel editionModel)
        {
            EditionModelValidation(editionModel);

            var newEdition = _mapper.Map<EditionModel, Edition>(editionModel);
            var oldEdition = await _editionRepos.FindByIdAsync(newEdition.Id);

            EditionExistCheck(oldEdition);

            await _editionRepos.UpdateAsync(oldEdition,newEdition);

        }
        public async Task RemoveEditionAsync(long id)
        {
            await _editionRepos.RemoveEditionById(id);
        }

        public async Task<List<EditionModel>> GetEditionListAsync()
        {
            var query = await _editionRepos.GetAllEditionsAsync();

            if (!query.Any())
                throw new CustomException(Constants.NOTHING_FOUND, HttpStatusCode.BadRequest);

            return _mapper.Map<List<Edition>, List<EditionModel>>(query);
        }
        public async Task<EditionModel> GetEditionByIdAsync(long id)
        {
            var getEdition = await _editionRepos.FindByIdAsync(id);
            EditionExistCheck(getEdition);

            return _mapper.Map<Edition, EditionModel>(getEdition);
        }
        public async Task<List<EditionModel>> GetEditionListByStringAsync(string searchString)
        {
            var query = (await _editionRepos.GetAllEditionsAsync()).Where(x => x.Id.ToString() == searchString)
                                                                   .Where(x => x.Title == searchString).ToList();
            

            if (!query.Any())
                throw new CustomException(Constants.NOTHING_FOUND, HttpStatusCode.OK);

            return _mapper.Map<List<Edition>, List<EditionModel>>(query);
        }
        public async Task<List<EditionModel>> GetEditionPageAsync(PageModel pageModel)
        {
            var query = await _editionRepos.Pagination(pageModel.PageNumber,pageModel.ElementsAmount,pageModel.SearchString);

            if (!query.Any())
                throw new CustomException(Constants.NOTHING_FOUND, HttpStatusCode.BadRequest);

            return _mapper.Map<List<Edition>, List<EditionModel>>(query);
        }

        private void EditionModelValidation(EditionModel editionModel)
        {        
            if (!editionModel.Title.Any() || editionModel.Title.Any(char.IsSymbol) || !editionModel.Title.Trim().Any())
                throw new CustomException("Invalid title!", HttpStatusCode.BadRequest);

            if (editionModel.Price < 0)
                throw new CustomException("Error! Price must be higher!", HttpStatusCode.BadRequest);
        }
        private void EditionExistCheck(Edition edition)
        {
            if (edition is null || edition.IsRemoved)
                throw new CustomException(Constants.NOTHING_FOUND, HttpStatusCode.BadRequest);
        }

    }
}
