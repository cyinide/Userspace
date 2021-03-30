using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Core.Models;

namespace Userspace.Api.Resources
{
    public class UserResource
    {
        public string UserId { get; set; }
        public virtual ICollection<LinkResource> Links { get; set; }
    }
}
