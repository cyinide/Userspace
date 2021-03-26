using System;
using System.Collections.Generic;
using System.Text;

namespace Userspace.Core.Models
{
    public class Link
    {
        public Link()
        {
            LinkTags = new HashSet<LinkTag>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<LinkTag> LinkTags { get; set; }
    }
}
