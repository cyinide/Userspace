using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Api.Resources.Auth
{
    public class SignInResource
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
