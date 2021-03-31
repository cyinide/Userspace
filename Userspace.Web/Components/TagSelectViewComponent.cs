using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Interfaces;
using Userspace.Web.Resources;

namespace Userspace.Web.Components
{
    public class ComponentModel
    {
        public string linkUrl { get; set; }
    }
  
    public class TagSelectViewComponent : ViewComponent
    {
        public ILinkService _linkService { get; private set; }
        public ITagService _tagService { get; private set; }
        public TagSelectViewComponent(ILinkService linkService, ITagService tagService)
        {
            _linkService = linkService;
            _tagService = tagService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string linkUrl)
        {
            var linkOccurance = await _linkService.CheckLinkForOccurance(linkUrl);
            if (linkOccurance != null)
            {
                var response = await _tagService.GetTagsByLinkId(linkOccurance.ID);
                if (response != null && response.Any())
                {
                    var tagResources = new List<TagResource>(response.ToList());
                    var mv = new ModelVariables();
                    mv.Options = tagResources.Select(x =>
                    new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.Name
                    }).ToList();

                    return View("TagPartial", mv);
                }
            }
            return View("TagPartial");
        }
    }
}
