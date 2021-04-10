using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models;

namespace Userspace.Web.Resources
{
    public class LinkResource
    {
        public LinkResource()
        {
            //TagResources = new List<TagResource>();
            TagResources = new List<SelectListItem>();
        }
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string UserId { get; set; }
        //public ICollection<TagResource> TagResources { get; set; }
        // public DropdownViewModel DropdownResource { get; set; }

        public string SelectedValue { get; set; }
        public List<SelectListItem> TagResources { get; set; }
    }
}

