using EdProject.BLL.Models.PrintingEditions;
using System.Collections.Generic;

namespace EdProject.BLL.Models.Editions
{
    public class EditionPageResponseModel
    {
        public long TotalPagesAmount { get; set; }
        public bool isNextPage { get; set; }
        public bool isPrevPage { get; set; }
        public long CurrentPage { get; set; }
        public List<EditionModel> EditionsPage { get; set; }
    }
}
