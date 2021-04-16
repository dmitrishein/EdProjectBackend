﻿using AutoMapper;
using EdProject.BLL.Models.PrintingEditions;
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
        public async Task DeletePrintEditionById(long id)
        {
            var res =  await _printEditionRepos.FindByIdAsync(id);

            if(res != null)
            {
                await _printEditionRepos.RemoveAsync(res);
            }

            return;
        }
        public async Task UpdatePrintEdition(PrintingEditionModel editionModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PrintingEditionModel, Edition>());
            var _mapper = new Mapper(config);
            var newEdition = _mapper.Map<PrintingEditionModel, Edition>(editionModel);

            await _printEditionRepos.UpdateAsync(newEdition);
        }
        public IEnumerable<Edition> GetEditionList()
        {
            return _printEditionRepos.Get();
        }
        public async Task<Edition> GetEditionById(long id)
        {
            return await _printEditionRepos.FindByIdAsync(id);
        }
        public IQueryable<Edition> GetEditionListByString(string searchString)
        {
            return _printEditionRepos.FilteredEditionList(searchString);
        }

    }
}
