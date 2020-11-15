using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UntitledProject.Models
{
    public class UserRoles : IdentityRole
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
