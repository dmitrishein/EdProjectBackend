using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.PrintingEditions;
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

            if (author is not null && author.IsRemoved)
            {
                await _authorRepository.DeleteAsync(author);
            }
            if (author is not null && !author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.ALREADY_EXIST, HttpStatusCode.BadRequest);
            }

            var newAuthor = _mapper.Map<AuthorModel, Author>(authorModel);
            await _authorRepository.CreateAsync(newAuthor);
        }
        public async Task CreateAuthorInEditionAsync(AuthorInEditionModel authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.AuthorId);
            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }
            if (author.Editions.Any(e=> e.Id == authorModel.EditionId))
            {
                throw new CustomException($"{ErrorConstant.CANNOT_ADD_EDITION}{ErrorConstant.ALREADY_EXIST}",HttpStatusCode.BadRequest);
            }

            var edition = await _editionRepository.FindByIdAsync(authorModel.EditionId);
            if(edition is null || edition.IsRemoved)
            {
                throw new CustomException(ErrorConstant.CANNOT_ADD_EDITION, HttpStatusCode.BadRequest);
            }
            await _authorRepository.AddEditionToAuthor(author,edition);
        }
        public async Task CreateAuthorInEditionsList(AuthorInEditionsList authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.AuthorId);
            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            string[] editionsId = authorModel.Editions.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

            List<Edition> editionAddList = new List<Edition>();
            foreach(var edition in editionsId)
            {
                var tempEdition =  await _editionRepository.FindByIdAsync(int.Parse(edition));

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    continue;
                }

                editionAddList.Add(tempEdition);
            }

            await _authorRepository.AddEditionListToAuthor(author, editionAddList);
        }

        public async Task<List<AuthorModel>> GetAuthorListAsync()
        {
            if (!(await _authorRepository.GetAllAuthorsAsync()).Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.OK);
            }

            return _mapper.Map<List<Author>, List<AuthorModel>>(await _authorRepository.GetAllAuthorsAsync());          
        }
        public async Task<AuthorModel> GetAuthorByIdAsync(long id)
        {
            var authorIn = await _authorRepository.FindByIdAsync(id);

            if (authorIn is null || authorIn.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            return _mapper.Map<Author, AuthorModel>(authorIn);
        }
        public async Task<List<EditionModel>> GetEditionsByAuthorIdAsync(long authorId)
        {
            var author = await _authorRepository.FindByIdAsync(authorId);
            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            List<Edition> editionsList = author.Editions.Where(e => !e.IsRemoved).ToList();

            return _mapper.Map<List<Edition>, List<EditionModel>>(editionsList);
        }
        public async Task<List<AuthorModel>> GetAuthorsByEditionIdAsync(long editionId)
        {
            var edition = await _editionRepository.FindByIdAsync(editionId);

            if (edition is null || edition.IsRemoved)
            { 
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest); 
            }
            List<Author> authorsList = edition.Authors.Where(a => !a.IsRemoved).ToList();

            return _mapper.Map<List<Author>, List<AuthorModel>>(authorsList);
        }

        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {   
            var updatedAuthor = _mapper.Map<AuthorModel, Author>(authorModel);
            var oldAuthor = await _authorRepository.FindByIdAsync(updatedAuthor.Id);

            if (oldAuthor is null || oldAuthor.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            await _authorRepository.UpdateAsync(oldAuthor,updatedAuthor);
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
        public async Task RemoveAuthorInEditionAsync(AuthorInEditionModel authorInEditionModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorInEditionModel.AuthorId);
            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            var edition = await _editionRepository.FindByIdAsync(authorInEditionModel.EditionId);
            if (edition is null)
            {
                throw new CustomException(ErrorConstant.ITEM_NOT_FOUND, HttpStatusCode.BadRequest);
            }
            await _authorRepository.RemoveAuthorInEdition(author,edition);
        }
        public async Task RemoveAuthorInEditionsList(AuthorInEditionsList authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.AuthorId);

            if (author is null || author.IsRemoved)
            {
                throw new CustomException(ErrorConstant.AUTHOR_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            string[] editionsId = authorModel.Editions.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
            List<Edition> editionToRemoveList = new List<Edition>();
            foreach (var edition in editionsId)
            {
                var tempEdition = await _editionRepository.FindByIdAsync(int.Parse(edition));

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    continue;
                }

                editionToRemoveList.Add(tempEdition);
            }

            await _authorRepository.RemoveAuthorInEditionList(author, editionToRemoveList);
        }

    }
}
