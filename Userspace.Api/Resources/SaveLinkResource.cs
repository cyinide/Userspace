using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Api.Resources
{
    public class SaveLinkResource
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string SelectedTag { get; set; }
        public ICollection<TagResource> TagResources { get; set; }
    }
}
