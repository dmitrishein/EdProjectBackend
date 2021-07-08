using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.AuthorDTO;
using EdProject.BLL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.ViewModels
{
    public class UsersViewModel
    {
        public List<UserModel> UsersList { get; set; }
    }
}
