using EdProject.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class AppRole : IdentityRole<long>
    {
        public UserRolesType RolesType { get; set; }
        public ICollection<UserInRole> Users { get; set; }
    }
}
