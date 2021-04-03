using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Web.Resources
{
    public class LinkResource
    {
        public LinkResource()
        {
            TagResources = new List<TagResource>();
            TagResources.Add(new TagResource { Name = "" }); // initial tag input - each link must have at least one tag
        }
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string UserId { get; set; }
        public ICollection<TagResource> TagResources { get; set; }
    }
}
