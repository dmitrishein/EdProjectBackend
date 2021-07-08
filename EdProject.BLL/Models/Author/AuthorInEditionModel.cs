using EdProject.BLL.Models.AuthorDTO;
using EdProject.BLL.Models.Editions;
using System.Collections.Generic;

namespace EdProject.BLL.Models.Author
{
    public class AuthorInEditionModel : AuthorModel
    {
        public List<EditionModel> Editions { get; set; }
    }
}
