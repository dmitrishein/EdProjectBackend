using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    public class AuthorController : Controller
    {
        IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task Create(AuthorViewModel createAuthor)
        {
            AuthorModel author = new()
            {
                Id = createAuthor.Id,
                Name = createAuthor.Name
            };

            await _authorService.CreateAuthorAsync(author);
        }

        [HttpGet("[action]")]
        public Task<IEnumerable<Author>> GetAuthorList()
        {
            return _authorService.GetAuthorList();
        }

        [HttpGet("[action]")]
        public async Task<Author> GetAuthorById(long id)
        {
            return await _authorService.GetAuthorById(id);
        }


        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task RemoveAuthorById(long id)
        {
            await _authorService.RemoveAuthorByIdAsync(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateAuthor(AuthorViewModel newAuthor)
        {
            AuthorModel authorModel = new()
            {
                Id = newAuthor.Id,
                Name = newAuthor.Name
            };
            await _authorService.UpdateAuthorAsync(authorModel);
        }

    }
}
