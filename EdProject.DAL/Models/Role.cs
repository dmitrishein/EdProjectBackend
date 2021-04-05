using EdProject.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class Role : IdentityRole<long>
    {
        public UserRolesType RolesType { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
