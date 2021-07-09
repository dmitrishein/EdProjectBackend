using EdProject.BLL.Models.Editions;
using EdProject.BLL.Models.ViewModels;
using EdProject.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IEditionService
    {
        public Task CreateEditionAsync(CreateEditionModel editionModel);
        public Task UpdateEditionAsync(CreateEditionModel editionModel);
        public Task RemoveEditionAsync(long id);
        public Task<CreateEditionModel> GetEditionByIdAsync(long id);
        public Task<EditionPageResponseModel> GetEditionPageAsync(EditionPageParameters pageModel);
        public Task<ProductsViewModel> GetViewModelAsync(EditionPageParameters pageParams);
    }
}
