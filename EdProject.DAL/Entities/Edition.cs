using EdProject.DAL.Entities.Base;
using EdProject.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Entities
{
    public class Edition:BaseEntity
    {
        public string Title { get; set; }

        public string Desciption { get; set; }

        public Decimal Price { get; set; }

        public EditionStatusTypes Status { get; set; }
        public CurrencyTypes Currency { get; set; }
        public EditionTypes Type { get; set; }

        //relationships
        public ICollection<AuthorInEditions> Authors { get; set; }
        public OrderItems OrderItem { get; set; }
    }
}
