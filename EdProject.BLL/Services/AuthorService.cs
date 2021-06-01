using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Interfaces;
using System;
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

            var authorEditions = await  _editionRepository.GetEditionRangeAsync(authorModel.EditionsId);
            if (!authorEditions.Any())
            {
                throw new CustomException(ErrorConstant.AUTHOR_CREATING_ERROR, HttpStatusCode.BadRequest);    
            }

            newAuthor.Editions = new List<Edition>();
            newAuthor.Editions.AddRange(authorEditions);

            await _editionRepository.SaveChangesAsync();
        }
        public async Task AddAuthorToEditionAsync(AuthorModel authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.Id);
            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            var editionsToAdd = await _editionRepository.GetEditionRangeAsync(authorModel.EditionsId);
            author.Editions.AddRange(editionsToAdd);

            await _editionRepository.SaveChangesAsync();

        }
        public async Task<List<AuthorModel>> GetAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return _mapper.Map<List<AuthorModel>>(authors);          
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
        public async Task<List<EditionModel>> GetEditionsByAuthorIdAsync(long authorId)
        {
            var author = await _authorRepository.FindByIdAsync(authorId);
            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            List<Edition> editionsList = author.Editions.Where(e => !e.IsRemoved).ToList();

            return _mapper.Map<List<EditionModel>>(editionsList);
        }
        public async Task<List<AuthorModel>> GetAuthorsByEditionIdAsync(long editionId)
        {
            var edition = await _editionRepository.FindByIdAsync(editionId);
            if (edition is null || edition.IsRemoved)
            { 
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest); 
            }

            List<Author> authorsList = edition.Authors.Where(a => !a.IsRemoved).ToList();

            return _mapper.Map<List<AuthorModel>>(authorsList);
        }

        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {   
            var oldAuthor = await _authorRepository.FindByIdAsync(authorModel.Id);
            var updEditionList = await _editionRepository.GetEditionRangeAsync(authorModel.EditionsId);

            if (oldAuthor is null || oldAuthor.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            oldAuthor.Name = authorModel.Name;
            oldAuthor.Editions = new List<Edition>();
            oldAuthor.Editions.AddRange(updEditionList);

            await _authorRepository.UpdateAsync(oldAuthor);
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
