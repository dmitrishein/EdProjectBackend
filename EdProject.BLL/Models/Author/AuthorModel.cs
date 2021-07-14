using EdProject.BLL.Models.Editions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.AuthorDTO
{
    public class AuthorModel : BaseModel
    {
        public string Name { get; set; }
    }
}
