using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.Author
{
    public class AddAuthorToEdition
    {
        public long EditionId { get; set; }
        public long AuthorId { get; set; }
    }
}
