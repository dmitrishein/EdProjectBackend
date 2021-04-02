using System;

namespace EdProject.DAL.Entities
{
    public class AuthorInEditions
    {
        public long EditionId { get; set; }
        public Edition Edition { get; set; }
        public long AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime Date { get; set; }
    }
}
