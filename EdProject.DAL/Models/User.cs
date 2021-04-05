using EdProject.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
