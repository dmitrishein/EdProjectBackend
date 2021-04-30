using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Middleware;
using EdProject.PresentationLayer.Models;
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
        public AuthorController(IAuthorService authorService, IAuthorInEditionService authorInEditionService)
        {
            _authorService = authorService;
            _authorInEditionService = authorInEditionService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task CreateAuthor(AuthorViewModel createAuthor)
        {
            AuthorModel author = new()
            {
                Name = createAuthor.FullName
            };

            await _authorService.CreateAuthorAsync(author);
        }

        [HttpGet("[action]")]
        public List<AuthorModel> GetAuthorList()
        {
            return _authorService.GetAuthorList();
        }

        [HttpGet("[action]")]
        public List<AuthorInEditionModel> GetEditionsByAuthorId(long authorId)
        {
            return _authorInEditionService.GetEditionsByAuthorId(authorId);
        }
        [HttpGet("[action]")]
        public List<AuthorInEditionModel> GetAuthorByEditionId(long editionId)
        {
            return _authorInEditionService.GetAuthorsByEditionId(editionId);
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
        public async Task UpdateAuthorAsync(AuthorViewModel newAuthor)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorViewModel, AuthorModel>());
            var _mapper = new Mapper(config);
            var newModel = _mapper.Map<AuthorViewModel, AuthorModel>(newAuthor);
            try
            {
                await _authorService.UpdateAuthorAsync(newModel);
            }
            catch (Exception x)
            {
                throw new CustomException($"Cannot update author. {x.Message}", 400);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateEditionAuthor(AuthorInEditionViewModel updateInfo)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditionViewModel, AuthorInEditionModel>());
            var _mapper = new Mapper(config);
            var updatedModel = _mapper.Map<AuthorInEditionViewModel, AuthorInEditionModel>(updateInfo);
            try
            {
                await _authorInEditionService.UpdateAuthorInEditAsync(updatedModel);
            }
            catch (Exception x)
            {
                throw new CustomException($"Cannot update author. {x.Message}", 400);
            }
        }

    }
}
