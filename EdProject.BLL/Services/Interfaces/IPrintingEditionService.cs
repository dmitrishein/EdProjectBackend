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
        public Task CreatePrintEdition(PrintingEditionModel editionModel);
        public Task DeletePrintEditionById(long id);
        public Task UpdatePrintEdition(PrintingEditionModel editionModel);
        public Task<Edition> GetEditionById(long id);
        public IEnumerable<Edition> GetEditionList();
        public IQueryable<Edition> GetEditionListByString(string searchString);
    }
}
