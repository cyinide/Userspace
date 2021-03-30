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
        public Guid UserId { get; set; }
    }
}
