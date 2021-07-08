using EdProject.BLL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserModel User { get; set; }
        public string NewPassword { get; set; }
    }
}
