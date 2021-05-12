using AutoMapper;
using EdProject.BLL.Models.Base;
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
        public  async Task CreateEdition(EditionModel newEdition)
        {        
            await _printEditionService.CreateEditionAsync(newEdition);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateEdition(EditionModel updateModel)
        {        
            await _printEditionService.UpdateEditionAsync(updateModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveEditionAsync(long id)
        {
            await _printEditionService.RemoveEditionAsync(id);
        }

        [HttpGet("[action]")]
        public Task<List<EditionModel>> GetEditions()
        {
           return _printEditionService.GetEditionListAsync();
        }

        [HttpGet("[action]")]
        public async Task<EditionModel> GetEditionById(long id)
        {
            return await _printEditionService.GetEditionByIdAsync(id);
        }

        [HttpGet("[action]")]
        public Task<List<EditionModel>> GetEditionByQuery(string searchString)
        {
            return _printEditionService.GetEditionListByStringAsync(searchString);
        }

        [HttpGet("[action]")]
        public Task<List<EditionModel>> GetEditionPage(PageModel pageModel)
        {
            return _printEditionService.GetEditionPageAsync(pageModel);
        }
    }
}
