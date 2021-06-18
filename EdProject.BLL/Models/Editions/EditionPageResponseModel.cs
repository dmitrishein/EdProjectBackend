﻿
using System.Collections.Generic;

namespace EdProject.BLL.Models.Editions
{
    public class EditionPageResponseModel
    {
        public long TotalItemsAmount { get; set; }
        public long CurrentPage { get; set; }
        public List<EditionModel> EditionsPage { get; set; }
    }
}
