using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class AuthorInEditionService : IAuthorInEditionService
    {
        AuthorInPrintingEditionRepository _authorInEditions;

        public AuthorInEditionService(AppDbContext appDbContext)
        {
            _authorInEditions = new AuthorInPrintingEditionRepository(appDbContext);
        }

        public async Task CreateAuthInEdAsync(AuthorInEditionModel authInEditModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditionModel, AuthorInEditions>());
            var _mapper = new Mapper(config);
            var newItem = _mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel);

            try
            {
                await _authorInEditions.CreateAsync(newItem);
            }
            catch(Exception x)
            {
                throw new Exception($"Can't add author to editon. {x.Message}");
            }
        }
        public async Task DeleteAuthInEditionAsync(AuthorInEditionModel authInEditModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditionModel, AuthorInEditions>());
            var _mapper = new Mapper(config);
            var itemToRemove = _mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel);

            await _authorInEditions.DeleteAsync(itemToRemove);

        }
        public List<AuthorInEditionModel> GetEditionsByAuthorId(long authorId)
        {
            try
            {
                List<AuthorInEditionModel> outList = new List<AuthorInEditionModel>();
                List<AuthorInEditions> queryList = _authorInEditions.GetAll().Where(x => x.AuthorId == authorId).ToList();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditions, AuthorInEditionModel>());
                var _mapper = new Mapper(config);

                if (queryList.Count() == 0)
                    throw new Exception("This author hasn't edition's");

                foreach(AuthorInEditions inEditions in queryList)
                {
                    var authorOut = _mapper.Map<AuthorInEditions, AuthorInEditionModel>(inEditions);
                    authorOut.EditionTitle = inEditions.Edition.Title;
                    authorOut.AuthorName = inEditions.Author.Name;
                    outList.Add(authorOut);
                }

                return outList;
            }
            catch (Exception x)
            {
                throw new Exception($"Error!. {x.Message}");
            }
        }
        public List<AuthorInEditionModel> GetAuthorsByEditionId(long editionId)
        {
            try
            {
                List<AuthorInEditionModel> outList = new List<AuthorInEditionModel>();
                List<AuthorInEditions> queryList = _authorInEditions.GetAll().Where(x => x.EditionId == editionId).ToList();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditions, AuthorInEditionModel>());
                var _mapper = new Mapper(config);

                if (queryList.Count() == 0)
                    throw new Exception("This edition without authors!");

                foreach (AuthorInEditions inEditions in queryList)
                {
                    var authorOut = _mapper.Map<AuthorInEditions, AuthorInEditionModel>(inEditions);
                    authorOut.EditionTitle = inEditions.Edition.Title;
                    authorOut.AuthorName = inEditions.Author.Name;
                    outList.Add(authorOut);
                }

                return outList;
            }
            catch (Exception x)
            {
                throw new Exception($"Error!. {x.Message}");
            }
        }
        public List<AuthorInEditionModel> GetList()
        {
            try
            {
                List<AuthorInEditionModel> outList = new List<AuthorInEditionModel>();
                List<AuthorInEditions> queryList = _authorInEditions.GetAll().ToList();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditions, AuthorInEditionModel>());
                var _mapper = new Mapper(config);

                if (queryList.Count() == 0)
                    throw new Exception("List is empty!");

                foreach (AuthorInEditions inEditions in queryList)
                {
                    var authorOut = _mapper.Map<AuthorInEditions, AuthorInEditionModel>(inEditions);
                    authorOut.EditionTitle = inEditions.Edition.Title;
                    authorOut.AuthorName = inEditions.Author.Name;
                    outList.Add(authorOut);
                }

                return outList;
            }
            catch (Exception x)
            {
                throw new Exception($"Error!. {x.Message}");
            }
        }
        public async Task UpdateAuthorInEditAsync(AuthorInEditionModel authInEditModel)
        {


            var oldItem = _authorInEditions.GetAll().Where(x => x.EditionId == authInEditModel.EditionId).FirstOrDefault();
            await _authorInEditions.DeleteAsync(oldItem);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorInEditionModel, AuthorInEditions>());
            var _mapper = new Mapper(config);
            var newItem = _mapper.Map<AuthorInEditionModel, AuthorInEditions>(authInEditModel);

            try
            {
                await _authorInEditions.CreateAsync(newItem);
            }
            catch(Exception x)
            {
                throw new Exception($"Failed to update. {x.Message}");
            }
        }
    }
}
