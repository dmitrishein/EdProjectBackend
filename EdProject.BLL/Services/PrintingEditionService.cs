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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Edition, PrintingEditionModel>());
            List<PrintingEditionModel> editions = _mapper.Map<List<Edition>,List<PrintingEditionModel>>(query);
            //var _mapper = new Mapper(config);


            //foreach (Edition edition in query)
            //{
            //    var editionModel = _mapper.Map<Edition, PrintingEditionModel>(edition); 
            //    editions.Add(editionModel);
            //}

            return editions;
        }
        public async Task<PrintingEditionModel> GetEditionAsync(long id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Edition, PrintingEditionModel>());
            var _mapper = new Mapper(config);

            var newUser = await _printEditionRepos.FindByIdAsync(id);
            if (newUser.IsRemoved)
                throw new Exception("Error! User was removed");

            var userEdition = _mapper.Map<Edition, PrintingEditionModel>(newUser);
            
            return userEdition;
        }
        public async Task<List<PrintingEditionModel>> GetEditionListByString(string searchString)
        {
            var query = await _printEditionRepos.GetAll().Where(x => x.Id.ToString() == searchString || x.Title == searchString || x.Price.ToString() == searchString).ToListAsync();
            List<PrintingEditionModel> editions = new List<PrintingEditionModel>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Edition, PrintingEditionModel>());
            var _mapper = new Mapper(config);

            foreach (Edition edition in query)
            {
                var editionModel = _mapper.Map<Edition, PrintingEditionModel>(edition);
                editions.Add(editionModel);
            }

            if (editions.Count == 0)
                throw new Exception("Nothing found:(");

            return editions;
        }

    }
}
