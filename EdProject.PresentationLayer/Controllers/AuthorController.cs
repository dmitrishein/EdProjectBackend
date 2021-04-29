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
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
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
            AuthorModel authorModel = new()
            {
                Id = newAuthor.Id,
                Name = newAuthor.FullName
            };
            try
            {
                await _authorService.UpdateAuthorAsync(authorModel);
            }
            catch (Exception x)
            {
                throw new CustomException($"Cannot update author. {x.Message}", 400);
            }
        }

    }
}
