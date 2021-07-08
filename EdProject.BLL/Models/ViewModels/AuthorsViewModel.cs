using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.AuthorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.ViewModels
{
    public class AuthorsViewModel
    {
        public List<AuthorInEditionModel> AuthorsList { get; set; }
    }
}
