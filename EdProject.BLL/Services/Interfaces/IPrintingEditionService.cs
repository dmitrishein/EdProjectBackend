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
        public Task<Edition> GetEditionById(long id);
        public Task<List<Edition>> GetEditionList();
        public Task<List<Edition>> GetEditionListByString(string searchString);
    }
}
