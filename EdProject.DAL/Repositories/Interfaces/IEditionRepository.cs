using EdProject.BLL.Models.Editions;
using EdProject.DAL.Entities;
using EdProject.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IEditionRepository : IBaseRepository<Edition>
    {
        public Edition FindEditionByTitle(string title);
        public Task<List<Edition>> GetEditionRangeAsync(List<long> editionsId);
        public Task<List<Edition>> GetAllEditionsAsync();
        public Task<EditionPageModel> Pagination(EditionPageParameters editionPageParameters);
        public Task<List<Edition>> GetAllEditionsInOrderAsync(long orderId);
        public Task<List<Edition>> GetAllAuthorEditionsAsync(long authorId);
    }
}
