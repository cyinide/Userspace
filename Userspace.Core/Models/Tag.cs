using System;
using System.Collections.Generic;
using System.Text;

namespace Userspace.Core.Models
{
    public class Tag
    {
        public Tag()
        {

        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int LinkId { get; set; }
        public Link Link { get; set; }
    }
}
