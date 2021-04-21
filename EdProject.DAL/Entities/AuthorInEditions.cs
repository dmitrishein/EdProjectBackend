using System;

namespace EdProject.DAL.Entities
{
    public class AuthorInEditions
    {
        public long EditionId { get; set; }
        public virtual Edition Edition { get; set; }
        public long AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public DateTime Date { get; set; }
    }
}
