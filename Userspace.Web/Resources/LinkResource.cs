using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Web.Resources
{
    public class LinkResource
    {
        public LinkResource()
        {
            TagResources = new List<TagResource>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<TagResource> TagResources { get; set; }
    }
}
