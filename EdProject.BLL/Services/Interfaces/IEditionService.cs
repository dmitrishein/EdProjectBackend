using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.PrintingEditions;
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
        public Task<List<EditionModel>> GetEditionPageAsync(FilterPageModel pageModel);
    }
}
