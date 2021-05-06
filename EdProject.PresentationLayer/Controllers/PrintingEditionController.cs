using AutoMapper;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        IPrintingEditionService _printEditionService;
        IMapper _mapper;
        public PrintingEditionController(IPrintingEditionService printingEditionService, IMapper mapper)
        {
            _printEditionService = printingEditionService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public  async Task Create(PrintingEditionViewModel register)
        {        
            await _printEditionService.CreatePrintEdition(_mapper.Map<PrintingEditionViewModel,PrintingEditionModel>(register));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateEdition(PrintingEditionViewModel updateModel)
        {        
            await _printEditionService.UpdatePrintEdition(_mapper.Map<PrintingEditionViewModel, PrintingEditionModel>(updateModel));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveEditionAsync(long id)
        {
            await _printEditionService.RemoveEditionAsync(id);
        }

        [HttpGet("[action]")]
        public Task<List<PrintingEditionModel>> GetEditions()
        {
           return _printEditionService.GetEditionListAsync();
        }

        [HttpGet("[action]")]
        public async Task<PrintingEditionModel> GetEdition(long id)
        {
            return await _printEditionService.GetEditionAsync(id);
        }

        [HttpGet("[action]")]
        public Task<List<PrintingEditionModel>> GetEditionByQuery(string searchString)
        {
            return _printEditionService.GetEditionListByStringAsync(searchString);
        }

    }
}
