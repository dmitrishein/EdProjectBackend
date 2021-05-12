using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task CreateEditionAsync(EditionModel editionModel);
        public Task UpdateEditionAsync(EditionModel editionModel);
        public Task RemoveEditionAsync(long id);
        public Task<EditionModel> GetEditionByIdAsync(long id);
        public Task<List<EditionModel>> GetEditionListAsync();
        public Task<List<EditionModel>> GetEditionListByStringAsync(string searchString);
        public Task<List<EditionModel>> GetEditionPageAsync(PageModel pageModel);
    }
}
