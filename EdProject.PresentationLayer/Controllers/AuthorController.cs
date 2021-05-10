using AutoMapper;
using EdProject.BLL;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
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
        IAuthorInEditionService _authorInEditionService;
        IMapper _mapper;
     
        public AuthorController(IAuthorService authorService, IAuthorInEditionService authorInEditionService,IMapper mapper)
        {
            _authorService = authorService;
            _authorInEditionService = authorInEditionService;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task CreateAuthor(AuthorModel createAuthor)
        {
            await _authorService.CreateAuthorAsync(createAuthor);
        }

        [HttpGet("[action]")]
        public Task<List<AuthorModel>> GetAuthorList()
        {
            return _authorService.GetAuthorList();
        }

        //[HttpGet("[action]")]
        //public Task<List<AuthorInEditionModel>> GetEditionsByAuthorId(long authorId)
        //{
        //    return _authorInEditionService.GetEditionsByAuthorId(authorId);
        //}

        //[HttpGet("[action]")]
        //public Task<List<AuthorInEditionModel>> GetAuthorByEditionId(long editionId)
        //{  
        //    //return _authorInEditionService.GetAuthorsByEditionId(editionId);
        //}

        [HttpPost("[action]")]
        public async Task AddAuthorToEdition(AuthorInEditionModel authorIn)
        {
            //await _authorInEditionService.CreateAuthInEdAsync(authorIn);
        }

        [HttpPost("[action]")]
        public async Task RemoveAuthorInEdition(AuthorInEditionModel authorIn)
        {
            //await _authorInEditionService.DeleteAuthInEditionAsync(authorIn);
        }

        [HttpGet("[action]")]
        public async Task<AuthorModel> GetAuthor(long id)
        {
            return await _authorService.GetAuthorById(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveAuthorAsync(long id)
        {
            await _authorService.RemoveAuthorAsync(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateAuthorAsync(AuthorModel newAuthor)
        {
           await _authorService.UpdateAuthorAsync(newAuthor);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateEditionAuthor(AuthorInEditionModel updateInfo)
        {
            //await _authorInEditionService.UpdateAuthorInEditAsync(updateInfo);
        }

    }
}
