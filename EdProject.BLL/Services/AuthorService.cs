﻿using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        AuthorRepository _authorRepository;
        IMapper _mapper;
        public AuthorService(AppDbContext appDb,IMapper mapper)
        {
            _authorRepository = new AuthorRepository(appDb);
            _mapper = mapper;
        }

        public async Task CreateAuthorAsync(AuthorModel authorModel)
        {
            char[] anyCh = { '/', '@', '#', '_' };
            if (Regex.IsMatch(authorModel.Name, @"[^0-9]") && authorModel.Name.IndexOfAny(anyCh) > 0)
                throw new CustomException("Author's name must to be without numbers",400);
           
            var newAuthor = _mapper.Map<AuthorModel, Author>(authorModel);

            if (_authorRepository.AuthorIsExist(newAuthor))
                throw new CustomException("Error! Author already exist!", 400);

            await _authorRepository.CreateAsync(newAuthor);
        }
        public async Task<AuthorModel> GetAuthorById(long id)
        {
            var authorIn = await _authorRepository.FindByIdAsync(id);

            if (authorIn is null)
                throw new CustomException("Author wasn't found!", 400);

            return _mapper.Map<Author, AuthorModel>(authorIn);
        }
        public async Task<List<AuthorModel>> GetAuthorList()
        {
            if (!(await _authorRepository.GetAllAuthorsAsync()).Any())
                throw new CustomException("Author wasn't found",200);

            return _mapper.Map<List<Author>, List<AuthorModel>>(await _authorRepository.GetAllAuthorsAsync());          
        }
        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {   
            var updatedAuthor = _mapper.Map<AuthorModel, Author>(authorModel);
            var oldAuthor = await _authorRepository.FindByIdAsync(updatedAuthor.Id);

            if (oldAuthor is null)
                throw new Exception("Error! Author wasn't found");

            if (oldAuthor.IsRemoved is true)
                throw new Exception("Cannot update. Author was removed");

            await _authorRepository.UpdateAsync(oldAuthor,updatedAuthor);
        }
        public async Task RemoveAuthorAsync(long id)
        {
            var author = await _authorRepository.FindByIdAsync(id);
            if (author.IsRemoved)
                throw new CustomException("author is already removed", 400);
            await _authorRepository.RemoveAuthorById(id);
        }

    }
}