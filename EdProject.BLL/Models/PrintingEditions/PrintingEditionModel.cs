using EdProject.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.PrintingEditions
{
    public class PrintingEditionModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public EditionStatusTypes Status { get; set; }
        public CurrencyTypes Currency { get; set; }
        public EditionStatusTypes Type { get; set; }

    }
}
