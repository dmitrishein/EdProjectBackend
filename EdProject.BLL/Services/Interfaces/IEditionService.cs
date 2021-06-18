using EdProject.BLL.Models.Editions;
using EdProject.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IEditionService
    {
        public Task CreateEditionAsync(EditionModel editionModel);
        public Task UpdateEditionAsync(EditionModel editionModel);
        public Task RemoveEditionAsync(long id);
        public Task<EditionModel> GetEditionByIdAsync(long id);
        public Task<List<EditionModel>> GetEditionsAsync();
        public Task<EditionPageResponseModel> GetEditionPageAsync(EditionPageParameters pageModel);
    }
}
