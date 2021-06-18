
using EdProject.DAL.Entities;
using System.Collections.Generic;

namespace EdProject.BLL.Models.Editions
{
    public class EditionPageModel
    {
        public long TotalItemsAmount { get; set; }
        public long CurrentPage { get; set; }
        public List<Edition> EditionsPage { get; set; }
    }
}
