using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Resources;

namespace Userspace.Web.Models
{
    public class LinkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int LinkId { get; set; }
        public Guid UserId { get; set; }
        public virtual LinkResource Link { get; set; }
    }
}
