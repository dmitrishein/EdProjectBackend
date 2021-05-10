using AutoMapper;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EditionController : ControllerBase
    {
        IPrintingEditionService _printEditionService;
        IMapper _mapper;
        public EditionController(IPrintingEditionService printingEditionService, IMapper mapper)
        {
            _printEditionService = printingEditionService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public  async Task Create(PrintingEditionModel register)
        {        
            await _printEditionService.CreatePrintEdition(register);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateEdition(PrintingEditionModel updateModel)
        {        
            await _printEditionService.UpdatePrintEdition(updateModel);
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
