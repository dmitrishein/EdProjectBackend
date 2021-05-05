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
        public Task UpdatePrintEdition(PrintingEditionModel editionModel);
        public Task RemoveEditionAsync(long id);
        public Task<PrintingEditionModel> GetEditionAsync(long id);
        public List<PrintingEditionModel> GetEditionList();
        public List<PrintingEditionModel> GetEditionListByString(string searchString);
    }
}
