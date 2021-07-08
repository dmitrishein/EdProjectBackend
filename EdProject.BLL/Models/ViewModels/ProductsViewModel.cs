using EdProject.BLL.Models.Editions;
using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Models;
using System.Collections.Generic;

namespace EdProject.BLL.Models.ViewModels
{
    public class ProductsViewModel
    {
        public long TotalItemsAmount { get; set; }
        public int ElementsPerPage { get; set; }
        public long CurrentPage { get; set; }
        public List<EditionModel> EditionsPage { get; set; }
    }
}
