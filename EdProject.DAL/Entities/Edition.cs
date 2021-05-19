using EdProject.DAL.Entities.Base;
using EdProject.DAL.Entities.Enums;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class Edition:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public AvailableStatusType Status { get; set; }
        public CurrencyTypes Currency { get; set; }
        public EditionTypes Type { get; set; }


        //relationships
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
