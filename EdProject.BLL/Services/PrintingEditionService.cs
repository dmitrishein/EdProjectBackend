using AutoMapper;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        
        PrintingEditionRepository _printEditionRepos;
        IMapper _mapper;
        public PrintingEditionService(AppDbContext appDbContext,IMapper mapper)
        {
            _printEditionRepos = new PrintingEditionRepository(appDbContext);
            _mapper = mapper;
        }

        public async Task CreatePrintEdition(PrintingEditionModel editionModel)
        {
            EditionValidation(editionModel);

            if (_printEditionRepos.IsExist(_mapper.Map<PrintingEditionModel, Edition>(editionModel)))
                throw new CustomException("Error! Edition with this title already exist!", 400);

            var newEdition = _mapper.Map<PrintingEditionModel, Edition>(editionModel);

            await _printEditionRepos.CreateAsync(newEdition);
                       
        }
        public async Task UpdatePrintEdition(PrintingEditionModel editionModel)
        {
            EditionValidation(editionModel);

            var newEdition = _mapper.Map<PrintingEditionModel, Edition>(editionModel);
            var oldEdition = await _printEditionRepos.FindByIdAsync(newEdition.Id);
           
            if (oldEdition is null)
                throw new CustomException("Edition was not found",400);
            if (oldEdition.IsRemoved)
                throw new CustomException("Edition was removed",400);

            await _printEditionRepos.UpdateAsync(oldEdition,newEdition);

        }
        public async Task RemoveEditionAsync(long id)
        {
            await _printEditionRepos.RemoveEditionById(id);
        }
        public async Task<List<PrintingEditionModel>> GetEditionListAsync()
        {
            var query = await _printEditionRepos.GetAllEditionsAsync();

            if (!query.Any())
                throw new CustomException("Edition's list is empty",200);

            return _mapper.Map<List<Edition>, List<PrintingEditionModel>>(query);
        }
        public async Task<PrintingEditionModel> GetEditionAsync(long id)
        {
            var getEdition = await _printEditionRepos.FindByIdAsync(id);
            if (getEdition.IsRemoved)
                throw new Exception("Error! Edition was removed");

            return _mapper.Map<Edition, PrintingEditionModel>(getEdition);
        }
        public async Task<List<PrintingEditionModel>> GetEditionListByStringAsync(string searchString)
        {
            var query = (await _printEditionRepos.GetAllEditionsAsync()).Where(x => x.Id.ToString() == searchString || 
            x.Title == searchString).ToList();

            if (!query.Any())
                throw new CustomException("Nothing found:(",200);

            return _mapper.Map<List<Edition>, List<PrintingEditionModel>>(query);      
        }

        private void EditionValidation(PrintingEditionModel editionModel)
        {        
            if (!editionModel.Title.Any())
                throw new CustomException("Error! Title shouldn't empty", 400);
            if (Regex.IsMatch(editionModel.Title, @"\.\W"))
                throw new CustomException($"Invalid title!", 400);
            if (editionModel.Price < 0)
                throw new CustomException("Error! Price must be higher!", 400);
        }

    }
}
