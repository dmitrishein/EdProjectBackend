using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.AuthorDTO;
using EdProject.BLL.Models.Editions;
using EdProject.BLL.Models.ViewModels;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        IAuthorRepository _authorRepository;
        IEditionRepository _editionRepository;
        IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository,IEditionRepository editionRepository,IMapper mapper)
        {
            _authorRepository = authorRepository;
            _editionRepository = editionRepository;
            _mapper = mapper;
        }

        public async Task CreateAuthorAsync(AuthorModel authorModel)
        {
            var author = _authorRepository.FindAuthorByName(authorModel.Name);
            if (author is not null)
            {
                throw new CustomException(ErrorConstant.ALREADY_EXIST, HttpStatusCode.BadRequest);
            }
            var newAuthor = _mapper.Map<Author>(authorModel);

            await _authorRepository.CreateAsync(newAuthor);
            await _authorRepository.SaveChangesAsync();
            
        }
        public async Task AddAuthorToEditionAsync(AddAuthorToEdition authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.AuthorId);
            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }
            var edition = await _editionRepository.FindByIdAsync(authorModel.EditionId);
            if (edition is null || edition.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_EDITION, HttpStatusCode.BadRequest);
            }
            edition.Authors.Add(author);
            await _editionRepository.SaveChangesAsync();

        }
        public async Task<AuthorModel> GetAuthorByIdAsync(long id)
        {
            var authorIn = await _authorRepository.FindByIdAsync(id);

            if (authorIn is null || authorIn.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            return _mapper.Map<AuthorModel>(authorIn);
        }

        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {   
            var oldAuthor = await _authorRepository.FindByIdAsync(authorModel.Id);

            if (oldAuthor is null || oldAuthor.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            oldAuthor.Name = authorModel.Name;
            oldAuthor.Editions = new List<Edition>();

            await _authorRepository.UpdateAsync(oldAuthor);
        }

        public async Task<AuthorsViewModel> GetAuthorsViewModel()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            var authorInEditions = _mapper.Map<List<AuthorInEditionModel>>(authors);
            return new AuthorsViewModel { AuthorsList = authorInEditions };
        }
        public async Task RemoveAuthorAsync(long id)
        {
            var author = await _authorRepository.FindByIdAsync(id);
            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }
            await _authorRepository.RemoveAuthorById(id);
        }
    }
}
