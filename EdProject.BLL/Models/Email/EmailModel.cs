using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.User
{
    public class EmailModel
    {
        public string RecipientName { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
    }
}
