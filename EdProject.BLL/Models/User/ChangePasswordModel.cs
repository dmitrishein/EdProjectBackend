using System.ComponentModel.DataAnnotations;

namespace EdProject.BLL.Models.User
{
    public class ChangePasswordModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
