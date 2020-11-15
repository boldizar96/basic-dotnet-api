using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace UntitledProject.Models
{
    public partial class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
