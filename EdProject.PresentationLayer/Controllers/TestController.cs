using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IEditionService _printEditionService;

        public TestController(IEditionService printEditionService)
        {
            _printEditionService = printEditionService;
        }

        [Authorize]
        [HttpGet("GetEditions")]
        public async Task<IActionResult> GetEditions()
        {
            var result = await _printEditionService.GetEditionsAsync();
            return Ok(result);
        }
    }
}
