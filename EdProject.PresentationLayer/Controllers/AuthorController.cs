using AutoMapper;
using EdProject.BLL;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        IAuthorService _authorService;
     
        public AuthorController(IAuthorService authorService,IMapper mapper)
        {
            _authorService = authorService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task CreateAuthor(AuthorModel createAuthor)
        {
            await _authorService.CreateAuthorAsync(createAuthor);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("[action]")]
        public async Task AddAuthorToEdition(AuthorInEditionModel authorIn)
        {
            await _authorService.CreateAuthorInEditionAsync(authorIn);
        }

        [HttpPost("[action]")]
        public async Task AddAuthorToEditionsList(AuthorInEditionsList authorIn)
        {
            await _authorService.CreateAuthorInEditionsList(authorIn);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateAuthorAsync(AuthorModel newAuthor)
        {
            await _authorService.UpdateAuthorAsync(newAuthor);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("[action]")]
        public Task<List<AuthorModel>> GetAuthorList()
        {
            return _authorService.GetAuthorListAsync();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("[action]")]
        public async Task<List<EditionModel>> GetEditionsByAuthorId(long authorId)
        {
            return await _authorService.GetEditionsByAuthorIdAsync(authorId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("[action]")]
        public async Task<List<AuthorModel>> GetAuthorsByEditionId(long editionId)
        {
            return await _authorService.GetAuthorsByEditionIdAsync(editionId);
        }


        [HttpGet("[action]")]
        public async Task<AuthorModel> GetAuthorAsync(long id)
        {
            return await _authorService.GetAuthorByIdAsync(id);
        }


        [HttpPost("[action]")]
        public async Task RemoveAuthorInEdition(AuthorInEditionModel authorIn)
        {
            await _authorService.RemoveAuthorInEditionAsync(authorIn);
        }

        [HttpPost("[action]")]
        public async Task RemoveAuthorInEditionsList(AuthorInEditionsList authorIn)
        {
            await _authorService.RemoveAuthorInEditionsList(authorIn);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveAuthorAsync(long id)
        {
            await _authorService.RemoveAuthorAsync(id);
        }


    }
}
