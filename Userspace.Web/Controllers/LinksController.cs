using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Userspace.Web.Interfaces;
using Userspace.Web.Models;
using Userspace.Web.Models.Auth;
using Userspace.Web.Resources;

namespace Userspace.Web.Controllers
{
    public class LinksController : Controller
    {
        public ILinkService _linkService { get; private set; }
        public ITagService _tagService { get; private set; }
        public IAuthService _authService { get; private set; }
        public LinksController(IAuthService authService, ILinkService linkService, ITagService tagService)
        {
            _linkService = linkService;
            _tagService = tagService;
            _authService = authService;
        }
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            TempData["Message"] = "Hello, " + Settings.CurrentUserName;
            var links = await _linkService.GetLinks(Settings.CurrentUserId);
            return View(links);
        }
        [HttpGet]
        public async Task<IActionResult> Tags()
        {
            TempData["Message"] = "Hello, " + Settings.CurrentUserName;
            var tags = await _tagService.GetTags();
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> TagsByLink(int linkId)
        {
            TempData["Message"] = "Hello, " + Settings.CurrentUserName;
            var tags = await _tagService.GetTagsByLinkId(linkId);
            return View("Tags", tags);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Login");
            var result = await _authService.Login(model);
            if (!result)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Register");
            var result = await _authService.Register(model);
            if (!result)
            {
                return RedirectToAction("Register");
            }
            return RedirectToAction("Home");
        }
        public async Task<ActionResult> Create([Bind("Name, TagResources")] LinkResource model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                if (ModelState.IsValid)
                {
                    var createdLink = await _linkService.CreateLink(model);
                    if (createdLink == null)
                    {
                        ViewBag.ErrorMessage = "Each link - tag relation must have a value. Link must have at least one tag associated with it. User cannot add same link multiple times.";
                    }
                    return RedirectToAction("Home");
                }
                else
                {
                    model.TagResources.Clear();
                    ViewBag.ErrorMessage = "Each link - tag relation must have a value. Link must have at least one tag associated with it. User cannot add same link multiple times.";
                }
            }
            return View(model);
        }
        public async Task<ActionResult> InitializeTags([Bind("Name, TagResources")] LinkResource model)
        {
            var linkOccurance = await _linkService.CheckLinkForOccurance(model.Name);
            if (linkOccurance != null)
            {
                var tagsWithOccurances = await _tagService.GetTagsByOccurancesAndLinkIdAsync(linkOccurance.ID);
                if (tagsWithOccurances != null && tagsWithOccurances.Any())
                {
                    model.TagResources = new List<TagResource>();
                    foreach (var item in tagsWithOccurances)
                    {
                        model.TagResources.Add(new TagResource { Name = item.Item1, NumberOfOccurances = item.Item2 });
                    }
                }
            }
            model.TagResources.Add(new TagResource());

            return PartialView("TagRow", model);
        }
        public async Task<ActionResult> AddTag([Bind("Name, TagResources")] LinkResource model)
        {
            model.TagResources.Add(new TagResource());

            return PartialView("TagRow", model);
        }
        public async Task<ActionResult> ClearTags([Bind("Name, TagResources")] LinkResource model)
        {
            if (model.TagResources != null)
                model.TagResources.Clear();

            return View("TagRow", model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
