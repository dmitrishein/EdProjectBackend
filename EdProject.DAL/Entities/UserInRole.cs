using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Entities
{
    public class UserInRole
    {
        public long UserId { get; set; }
        public AppUser AppUser { get; set; }
        public long AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
    }
}
