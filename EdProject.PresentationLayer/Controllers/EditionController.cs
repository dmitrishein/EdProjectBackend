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

        [HttpPost("[action]")]
        public async Task<EditionPageResponseModel> GetEditionPage(EditionPageParameters pageModel)
        {
            var response = await _printEditionService.GetEditionPageAsync(pageModel);
            return response;
        }
    }
}
