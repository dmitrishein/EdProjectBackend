using EdProject.DAL.Entities.Base;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class Author:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Edition> Editions { get; set; }
    }
}
