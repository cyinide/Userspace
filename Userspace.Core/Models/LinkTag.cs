using System;
using System.Collections.Generic;
using System.Text;

namespace Userspace.Core.Models
{
    public class LinkTag
    {
        public int LinkId { get; set; }
        public int TagId { get; set; }
        public virtual Link Link { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
