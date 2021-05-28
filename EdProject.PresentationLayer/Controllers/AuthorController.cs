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


        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task CreateAuthor(AuthorModel createAuthor)
        {
            await _authorService.CreateAuthorAsync(createAuthor);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task AddAuthorToEdition(AuthorModel createAuthor)
        {
            await _authorService.AddAuthorToEdition(createAuthor);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateAuthorAsync(AuthorModel newAuthor)
        {
            await _authorService.UpdateAuthorAsync(newAuthor);
        }

        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public Task<List<AuthorModel>> GetAuthorList()
        {
            return _authorService.GetAuthorListAsync();
        }

        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public async Task<List<EditionModel>> GetEditionsByAuthorId(long authorId)
        {
            return await _authorService.GetEditionsByAuthorIdAsync(authorId);
        }

        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public async Task<List<AuthorModel>> GetAuthorsByEditionId(long editionId)
        {
            return await _authorService.GetAuthorsByEditionIdAsync(editionId);
        }


        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public async Task<AuthorModel> GetAuthorAsync(long id)
        {
            return await _authorService.GetAuthorByIdAsync(id);
        }

        [Authorize(Roles = "admin")]
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
