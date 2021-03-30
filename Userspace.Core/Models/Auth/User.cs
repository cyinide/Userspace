using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Userspace.Core.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            UserLinks = new HashSet<UserLink>();
        }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<UserLink> UserLinks { get; set; }
    }
}
