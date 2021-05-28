using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isRemoved { get; set; }

        public virtual List<Orders> Orders { get; set; }

    }
}
