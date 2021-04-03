﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Web.Resources
{
    public class TagPartial
    {
        public string TagName { get; set; }
        public string LinkId { get; set; }
    }

    public class ModelVariables
    {
        public IEnumerable<SelectListItem> Options { set; get; }
        public string[] SelectedOptions { set; get; }
        public int LinkId { get; set; }
        public string TagName { get; set; }

    }

    public class TagResource
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int LinkId { get; set; }
        public int NumberOfOccurances { get; set; } = 5;

        public bool IsDeleted { get; set; }
    }
}
