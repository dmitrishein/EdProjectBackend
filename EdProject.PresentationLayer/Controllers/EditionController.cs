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
        IEditionService _printEditionService;
        public EditionController(IEditionService printingEditionService)
        {
            _printEditionService = printingEditionService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task CreateEdition(EditionModel newEdition)
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
           return _printEditionService.GetEditionsAsync();
        }


        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public async Task<EditionModel> GetEditionById(long id)
        {
            return await _printEditionService.GetEditionByIdAsync(id);
        }

        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public Task<List<EditionModel>> GetEditionPage(FilterPageModel pageModel)
        {
            return _printEditionService.GetEditionPageAsync(pageModel);
        }
    }
}
