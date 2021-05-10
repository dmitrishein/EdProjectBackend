using EdProject.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class Role : IdentityRole<long>
    {
        public Role(string roleName) : base(roleName)
        {
            Name = roleName;
        }  
        public Role()
        {
        }

        public UserRolesType RolesType { get; set; }
        public bool isRemoved { get; set; } 
    }
}
