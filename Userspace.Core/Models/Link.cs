using System;
using System.Collections.Generic;
using System.Text;

namespace Userspace.Core.Models
{
    public class Link
    {
        public Link()
        {
            Tags = new HashSet<Tag>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
