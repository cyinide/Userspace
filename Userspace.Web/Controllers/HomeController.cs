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
    public class HomeController : Controller
    {
        public ILinkService _linkService { get; private set; }
        public ITagService _tagService { get; private set; }
        public IAuthService _authService { get; private set; }

        public HomeController(IAuthService authService, ILinkService linkService, ITagService tagService)
        {
            _linkService = linkService;
            _tagService = tagService;
            _authService = authService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            TempData["Message"] = "Hello ," + Settings.CurrentUserName;
            var links = await _linkService.GetLinks(Settings.CurrentUserId);

            return View(links);
        }
        [HttpGet]
        public async Task<IActionResult> Tags()
        {
            TempData["Message"] = "Hello ," + Settings.CurrentUserName;
            var tags = await _tagService.GetTags();
            return View(tags);
        }
        [HttpGet]

        public async Task<IActionResult> TagsPartial(string linkUrl)
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

                    return View(mv);
                }
            }
            return View();
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
                ViewBag.Message = string.Format("Credentials are in incorrect format.");
            var result = await _authService.Login(model);
            if (!result)
            {
                ViewBag.Message = string.Format("Incorrect username or password.");
                return RedirectToAction("Login");
            }
            ViewBag.Message = string.Format("User signed up.");

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                ViewBag.Message = string.Format("Credentials are in incorrect format.");
            var result = await _authService.Register(model);
            if (!result)
            {
                ViewBag.Message = string.Format("Try again.");
                return RedirectToAction("Register");
            }
            ViewBag.Message = string.Format("User signed in.");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(LinkResource model)
        {
            var createdLink = await _linkService.CreateLink(model);
            return RedirectToAction("Index");
        }
        public IActionResult CreateTag()
        {
            return PartialView("TagCreate", new TagResource { });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
