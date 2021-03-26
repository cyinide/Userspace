using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Userspace.Web.Interfaces;
using Userspace.Web.Models;
using Userspace.Web.Models.Auth;

namespace Userspace.Web.Controllers
{
    public class HomeController : Controller
    {
        public ILinkService _linkService { get; private set; }
        public ITagService _tagService { get; private set; }

        public HomeController(ILinkService linkService, ITagService tagService)
        {

            _linkService = linkService;
            _tagService = tagService;
        }
        public async Task<IActionResult> Index()
        {
            var links = await _linkService.GetLinks();
            return View(links);
        }
        public async Task<IActionResult> Tags()
        {
            var tags = await _tagService.GetTags();
            return View(tags);
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
