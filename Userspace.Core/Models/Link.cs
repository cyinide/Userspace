using System;
using System.Collections.Generic;
using System.Text;
using Userspace.Core.Models.Auth;

namespace Userspace.Core.Models
{
    public class Link
    {
        public Link()
        {
            Tags = new HashSet<Tag>();
            UserLinks = new HashSet<UserLink>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<UserLink> UserLinks { get; set; }
    }
}
