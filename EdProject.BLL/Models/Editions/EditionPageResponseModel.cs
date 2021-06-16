using EdProject.BLL.Models.PrintingEditions;
using System.Collections.Generic;

namespace EdProject.BLL.Models.Editions
{
    public class EditionPageResponseModel
    {
        public long TotalPagesAmount { get; set; }
        public List<EditionModel> Editions { get; set; }
    }
}
