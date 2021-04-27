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
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        
        PrintingEditionRepository _printEditionRepos;

        public PrintingEditionService(AppDbContext appDbContext)
        {
            _printEditionRepos = new PrintingEditionRepository(appDbContext);
        }

        public async Task CreatePrintEdition(PrintingEditionModel editionModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PrintingEditionModel, Edition>());
            var _mapper = new Mapper(config);
            var newEdition = _mapper.Map<PrintingEditionModel, Edition>(editionModel);

            await _printEditionRepos.CreateAsync(newEdition);
        }
        
        public async Task UpdatePrintEdition(PrintingEditionModel editionModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PrintingEditionModel, Edition>());
            var _mapper = new Mapper(config);
            var newEdition = _mapper.Map<PrintingEditionModel, Edition>(editionModel);

            await _printEditionRepos.UpdateAsync(newEdition);
        }
        public Task<List<Edition>> GetEditionList()
        {
            return _printEditionRepos.GetAll().ToListAsync();
        }
        public async Task<Edition> GetEditionById(long id)
        {
            return await _printEditionRepos.FindByIdAsync(id);
        }
        public Task<List<Edition>> GetEditionListByString(string searchString)
        {
            return _printEditionRepos.FilterEditionList(searchString);
        }

    }
}
