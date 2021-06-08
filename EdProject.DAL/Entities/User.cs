using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EdProject.DAL.Entities
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }
        public bool isRemoved { get; set; }

        public virtual List<Orders> Orders { get; set; }

    }
}
