using EdProject.BLL.Models.AuthorDTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.Editions
{
    public class EditionModel:BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<AuthorModel> Authors { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }

    }
}
