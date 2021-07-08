
using EdProject.DAL.Entities;
using System.Collections.Generic;

namespace EdProject.BLL.Models.Editions
{
    public class EditionPageModel
    {
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public long TotalItemsAmount { get; set; }
        public int ElementsPerPage { get; set; }
        public long CurrentPage { get; set; }
        public List<Edition> EditionsPage { get; set; }
    }
}
