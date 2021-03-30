using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Api.Resources
{
    public class UserLinkResource
    {
        public Guid UserId { get; set; }
        public int LinkId { get; set; }
        public virtual LinkResource Link { get; set; }
    }
}
