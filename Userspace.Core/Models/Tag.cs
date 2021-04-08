using System;
using System.Collections.Generic;
using System.Text;

namespace Userspace.Core.Models
{
    public class Tag
    {
        public Tag()
        {
            UserLinks = new HashSet<UserLink>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserLink> UserLinks { get; set; }
    }
}
