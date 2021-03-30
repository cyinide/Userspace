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
            Users = new HashSet<User>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
