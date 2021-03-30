using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Api.Resources
{
    public class LinkResource
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<TagResource> TagResources { get; set; }
        public ICollection<UserResource> UserResources { get; set; }
    }
}
