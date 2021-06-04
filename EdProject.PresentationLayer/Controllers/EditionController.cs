﻿using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        [Authorize]
        [HttpGet("GetEditions")]
        public async Task<List<EditionModel>> GetEditions()
        {
            var result= await _printEditionService.GetEditionsAsync();
            return result;
        }


        [Authorize]
        [HttpGet("[action]")]
        public async Task<EditionModel> GetEditionById(long id)
        {
            return await _printEditionService.GetEditionByIdAsync(id);
        }

        [Authorize]
        [HttpGet("[action]")]
        public Task<List<EditionModel>> GetEditionPage(FilterPageModel pageModel)
        {
            return _printEditionService.GetEditionPageAsync(pageModel);
        }
    }
}
