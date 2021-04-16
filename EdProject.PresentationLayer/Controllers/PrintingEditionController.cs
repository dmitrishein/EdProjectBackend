using AutoMapper.Configuration;
using EdProject.BLL;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    public class PrintingEditionController : ControllerBase
    {
        IPrintingEditionService _printEditionService;
        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printEditionService = printingEditionService;
        }
     


        [HttpPost("[action]")]
        public  async Task Create(PrintingEditionViewModel register)
        {
            PrintingEditionModel editionModel = new PrintingEditionModel
            {
                Title = register.Title,
                Description = register.Description,
                Price = register.Price,
                Status = register.Status,
                Currency = register.Currency,
                Type = register.Types
            };

            await _printEditionService.CreatePrintEdition(editionModel);
        }

        [HttpPost("[action]")]
        public async Task DeleteById(long id)
        {
            await _printEditionService.DeletePrintEditionById(id);
        }

        [HttpPost("[action]")]
        public async Task UpdateEdition(PrintingEditionViewModel updateModel)
        {
            PrintingEditionModel editionModel = new PrintingEditionModel
            {
                Id = updateModel.Id,
                Title = updateModel.Title,
                Description = updateModel.Description,
                Price = updateModel.Price,
                Status = updateModel.Status,
                Currency = updateModel.Currency,
                Type = updateModel.Types
            };
            await _printEditionService.UpdatePrintEdition(editionModel);
        }

        [HttpGet("[action]")]
        public IEnumerable<Edition> GetEditions()
        {
           return _printEditionService.GetEditionList();
        }

        [HttpGet("[action]")]
        public async Task<Edition> GetEditionById(long id)
        {
            return await _printEditionService.GetEditionById(id);
        }

        [HttpGet("[action]")]
        public IQueryable<Edition> GetEditionByQuery(string searchString)
        {
            return _printEditionService.GetEditionListByString(searchString);
        }

    }
}
