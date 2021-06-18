using AutoMapper;
using EdProject.BLL.Models.Editions;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Models;
using EdProject.DAL.Repositories.Base;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories
{
    public class EditionRepository : BaseRepository<Edition>, IEditionRepository
    {
        IMapper _mapper;
        public EditionRepository(AppDbContext appDbContext, IMapper mapper) : base (appDbContext)
        {
            _mapper = mapper;
        }
 
        public Edition FindEditionByTitle(string title)
        {
            return GetAll().FirstOrDefault(e => e.Title == title && !e.IsRemoved);
        }  
        public async Task<List<Edition>> GetEditionRangeAsync(List<long> editionsId)
        {
            var editionList = GetAll().Where(ed => editionsId.Contains(ed.Id));

            return await editionList.ToListAsync();
        }
        public async Task<List<Edition>> GetAllEditionsAsync()
        {
            return await GetAll().Where(x =>!x.IsRemoved).ToListAsync(); 
        }
        public async Task<List<Edition>> GetAllAuthorEditionsAsync(long authorId)
        {
            return await GetAll().Where(x => !x.IsRemoved && x.Authors.Any(y => y.Id == authorId)).ToListAsync();
        }
        public async Task<List<Edition>> GetAllEditionsInOrderAsync(long orderId)
        {
            return await GetAll().Where(x => x.Orders.Any(y=> y.Id == orderId)).ToListAsync();
        }
        public async Task<EditionPageModel> Pagination(EditionPageParameters editionPageParameters)
        {

            var countItems = await GetAll().Where(e => e.Title.Contains(editionPageParameters.SearchString) ||
                                      e.Authors.Any(a => a.Name.Contains(editionPageParameters.SearchString)) ||
                                      e.Id.ToString().Contains(editionPageParameters.SearchString))
                          .Where(e => editionPageParameters.EditionTypes.Contains(e.Type))
                          .Where(e => e.Price >= editionPageParameters.MinPrice)
                          .Where(e => editionPageParameters.MaxPrice <= VariableConstant.MIN_PRICE ? e.Price == e.Price : e.Price < editionPageParameters.MaxPrice)
                          .Where(e => !e.IsRemoved)
                          .CountAsync();

            var listResults = GetAll().Where(e => e.Title.Contains(editionPageParameters.SearchString) ||
                                                  e.Authors.Any(a=>a.Name.Contains(editionPageParameters.SearchString)) ||
                                                  e.Id.ToString().Contains(editionPageParameters.SearchString))
                                      .Where(e => editionPageParameters.EditionTypes.Contains(e.Type))
                                      .Where(e => e.Price >= editionPageParameters.MinPrice)
                                      .Where(e => editionPageParameters.MaxPrice <= VariableConstant.MIN_PRICE ? e.Price == e.Price : e.Price < editionPageParameters.MaxPrice)
                                      .Where(e => !e.IsRemoved)
                                      .Skip((editionPageParameters.CurrentPageNumber - VariableConstant.SKIP_ZERO_PAGE) * editionPageParameters.ElementsPerPage)
                                      .Take(editionPageParameters.ElementsPerPage);

            EditionPageModel editionPageModel = new EditionPageModel
            {
                TotalItemsAmount = countItems,
                CurrentPage = editionPageParameters.CurrentPageNumber,
                EditionsPage = await listResults.ToListAsync()
            };  
            return editionPageModel; 
        }
    }
}
