using AutoMapper;
using AutoMapper.Configuration;
using EdProject.BLL;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateEdition(PrintingEditionViewModel updateModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PrintingEditionViewModel, PrintingEditionModel>());
            var _mapper = new Mapper(config);
            var updateEdition = _mapper.Map<PrintingEditionViewModel, PrintingEditionModel>(updateModel);
           
            await _printEditionService.UpdatePrintEdition(updateEdition);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveEditionAsync(long id)
        {
            await _printEditionService.RemoveEditionAsync(id);
        }
        [HttpGet("[action]")]
        public List<PrintingEditionModel> GetEditions()
        {
           return _printEditionService.GetEditionList();
        }

        [HttpGet("[action]")]
        public async Task<PrintingEditionModel> GetEdition(long id)
        {
            return await _printEditionService.GetEditionAsync(id);
        }

        [HttpGet("[action]")]
        public List<PrintingEditionModel> GetEditionByQuery(string searchString)
        {
            return _printEditionService.GetEditionListByString(searchString);
        }

    }
}
