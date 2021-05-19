using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        AuthorRepository _authorRepository;
        EditionRepository _editionRepository;
        IMapper _mapper;
        public AuthorService(AppDbContext appDb,IMapper mapper)
        {
            _authorRepository = new AuthorRepository(appDb);
            _editionRepository = new EditionRepository(appDb);
            _mapper = mapper;
        }

        public async Task CreateAuthorAsync(AuthorModel authorModel)
        {
            AuthorValidation(authorModel);
            var newAuthor = _mapper.Map<AuthorModel, Author>(authorModel);

            if (_authorRepository.AuthorIsExist(newAuthor))
                throw new CustomException("Error! Author already exist!", HttpStatusCode.BadRequest);

            await _authorRepository.CreateAsync(newAuthor);
        }
        public async Task CreateAuthorInEditionAsync(AuthorInEditionModel authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.AuthorId);
            AuthorExistCheck(author);
            var edition = await _editionRepository.FindByIdAsync(authorModel.EditionId);
            await _authorRepository.AddEditionToAuthor(author,edition);
        }
        public async Task CreateAuthorInEditionsList(AuthorInEditionsList authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.AuthorId);
            AuthorExistCheck(author);            
            string[] editionsId = authorModel.Editions.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
            List<Edition> editionList = new List<Edition>();
          
            foreach(var edition in editionsId)
            {
                var tempEdition =  await _editionRepository.FindByIdAsync(int.Parse(edition));

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    throw new CustomException($"Incorrect edition Id{edition}", HttpStatusCode.BadRequest);
                }

                editionList.Add(tempEdition);
            }
            await _authorRepository.AddEditionListToAuthor(author, editionList);
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
            AuthorExistCheck(authorIn);
            return _mapper.Map<Author, AuthorModel>(authorIn);
        }
        public async Task<List<EditionModel>> GetEditionsByAuthorIdAsync(long authorId)
        {
            var author = await _authorRepository.FindByIdAsync(authorId);
            AuthorExistCheck(author);

            List<Edition> editionsList = author.Editions.Where(e => !e.IsRemoved).ToList();
            return _mapper.Map<List<Edition>, List<EditionModel>>(editionsList);
        }
        public async Task<List<AuthorModel>> GetAuthorsByEditionIdAsync(long editionId)
        {
            var edition = await _editionRepository.FindByIdAsync(editionId);

            if (edition is null || edition.IsRemoved)
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);

            List<Author> authorsList = edition.Authors.Where(a => !a.IsRemoved).ToList();
            return _mapper.Map<List<Author>, List<AuthorModel>>(authorsList);
        }

        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {   
            var updatedAuthor = _mapper.Map<AuthorModel, Author>(authorModel);
            var oldAuthor = await _authorRepository.FindByIdAsync(updatedAuthor.Id);

            AuthorExistCheck(oldAuthor);

            await _authorRepository.UpdateAsync(oldAuthor,updatedAuthor);
        }

        public async Task RemoveAuthorAsync(long id)
        {
            var author = await _authorRepository.FindByIdAsync(id);
            AuthorExistCheck(author);
            await _authorRepository.RemoveAuthorById(id);
        }
        public async Task RemoveAuthorInEditionAsync(AuthorInEditionModel authorInEditionModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorInEditionModel.AuthorId);
            AuthorExistCheck(author);
            var edition = await _editionRepository.FindByIdAsync(authorInEditionModel.EditionId);

            await _authorRepository.RemoveAuthorInEdition(author,edition);
        }
        public async Task RemoveAuthorInEditionsList(AuthorInEditionsList authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.AuthorId);

            AuthorExistCheck(author);

            string[] editionsId = authorModel.Editions.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
            List<Edition> editionList = new List<Edition>();

            foreach (var edition in editionsId)
            {
                var tempEdition = await _editionRepository.FindByIdAsync(int.Parse(edition));

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    throw new CustomException($"Incorrect edition Id {edition}", HttpStatusCode.BadRequest);
                }

                editionList.Add(tempEdition);
            }

            await _authorRepository.RemoveAuthorInEditionList(author, editionList);
        }


        private void AuthorValidation(AuthorModel authorModel)
        {
            if (authorModel.Name.Any(char.IsDigit) || authorModel.Name.Any(char.IsPunctuation))
                throw new CustomException("Invalid Author's name! Name consist of letter only! ", HttpStatusCode.BadRequest);
            if (!authorModel.Name.Trim().Any(char.IsLetter)) 
                throw new CustomException("Invalid Author's name!", HttpStatusCode.BadRequest);
        }
        private void AuthorExistCheck(Author author)
        {
            if (author is null)
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.NoContent);
            if (author.IsRemoved)
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.NoContent);
        }
    }
}
