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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PrintingEditionModel, Edition>());
            var _mapper = new Mapper(config);
            var newEdition = _mapper.Map<PrintingEditionModel, Edition>(editionModel);
            try
            {
                await _printEditionRepos.CreateAsync(newEdition);
            }
            catch (Exception x)
            {
                throw new Exception($"Cannot create edition. {x.Message}");
            }
        }
        public async Task UpdatePrintEdition(PrintingEditionModel editionModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PrintingEditionModel, Edition>());
            var _mapper = new Mapper(config);
            var newEdition = _mapper.Map<PrintingEditionModel, Edition>(editionModel);

            var editionCheck = await _printEditionRepos.FindByIdAsync(newEdition.Id);
           
            if (editionCheck is null)
                throw new Exception("Edition is not found");
            if (editionCheck.IsRemoved)
                throw new Exception("Edition is removed");
            try
            {
                await _printEditionRepos.UpdateAsync(editionCheck,newEdition);
            }
            catch(Exception x)
            {
                throw new Exception($"Couldn't update edition. Error: {x.Message}");
            }
        }
        public async Task RemoveEditionAsync(long id)
        {
            try
            {
                await _printEditionRepos.RemoveEditionById(id);
            }
            catch (Exception x)
            {
                throw new Exception($"Error! Cannot remove Edition { x.Message}");
            }
        }
        public List<PrintingEditionModel> GetEditionList()
        {
            var query =  _printEditionRepos.GetAllEditions();
            if (!query.Any())
                throw new Exception("Edition's list is empty");
            
            List<PrintingEditionModel> editions = _mapper.Map<List<Edition>, List<PrintingEditionModel>>(query);

            return editions;
        }
        public async Task<PrintingEditionModel> GetEditionAsync(long id)
        {
            var getEdition = await _printEditionRepos.FindByIdAsync(id);
            if (getEdition.IsRemoved)
                throw new Exception("Error! User was removed");

            var userEdition = _mapper.Map<Edition, PrintingEditionModel>(getEdition);
            return userEdition;
        }
        public List<PrintingEditionModel> GetEditionListByString(string searchString)
        {
            var query = _printEditionRepos.GetAllEditions().Where(x => x.Id.ToString() == searchString || x.Title == searchString || x.Price.ToString() == searchString).ToList();

            List<PrintingEditionModel> editions = _mapper.Map<List<Edition>, List<PrintingEditionModel>>(query);

            if (!editions.Any())
                throw new Exception("Nothing found:(");

            return editions;
        }

    }
}
