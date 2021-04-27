using EdProject.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class AppRole : IdentityRole<long>
    {
        public AppRole(string roleName) : base(roleName)
        {
            Name = roleName;
        }  
        public AppRole()
        {

        }

        public UserRolesType RolesType { get; set; }
        public bool isRemoved { get; set; } 
    }
}
