using EdProject.BLL.Models.Editions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EditionController : Controller
    {
        private readonly IEditionService _printEditionService;
        public EditionController(IEditionService printingEditionService)
        {
            _printEditionService = printingEditionService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveEditionAsync(long id)
        {
            await _printEditionService.RemoveEditionAsync(id);
        }

        [HttpGet("GetEditions")]
        public async Task<List<EditionModel>> GetEditions()
        {
            var result= await _printEditionService.GetEditionsAsync();
            return result;
        }

        [HttpGet("[action]")]
        public async Task<EditionModel> GetEditionById(long id)
        {
            //return await _printEditionService.GetEditionByIdAsync(id);
            return null;
        }

        [HttpPost("[action]")]
        public Task<EditionPageResponseModel> GetEditionPage(EditionPageParameters pageModel)
        {
            return _printEditionService.GetEditionPageAsync(pageModel);
        }
    }
}
