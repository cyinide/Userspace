using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using StopWord;
using Userspace.Web.Extensions;
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
            var links = await _linkService.GetLinks(Settings.CurrentUserId);
            return View(links);
        }
        [HttpGet]
        public async Task<IActionResult> Tags()
        {
            var tags = await _tagService.GetTags();
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> TagsByLink(int linkId)
        {
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
            {
                return RedirectToAction("Login");
            }
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
            return RedirectToAction("Login");
        }
        public async Task<ActionResult> Create([Bind("Name, TagResources")] LinkResource model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                if (ModelState.IsValid)
                {
                    if(model.TagResources == null || !model.TagResources.Any())
                    {
                        ViewBag.ErrorMessage = Settings.ErrorMessage;
                        return View(model);
                    }
                    var createdLink = await _linkService.CreateLink(model);
                    if (createdLink == null)
                    {
                        ViewBag.ErrorMessage = Settings.ErrorMessage;
                        return View(model);
                    }
                    model.ID = createdLink.ID;
                    //var contentTags = StripHtml(model);
                    //if (contentTags != null && contentTags.Any())
                    //{
                    //    foreach (var item in contentTags)//TODO: CreateTagRange
                    //    {
                    //        item.LinkId = createdLink.ID;
                    //       await _tagService.CreateTag(item); 
                    //    }
                    //}
                    return RedirectToAction("Home");
                }
                else
                {
                    model.TagResources.Clear();
                    ViewBag.ErrorMessage = Settings.ErrorMessage;
                }
            }
            return View(model);
        }
        protected List<TagResource> StripHtml(LinkResource link)
        {
            string siteContent = string.Empty;
            try
            {
                List<TagResource> tagResourcesForSuggestion = new List<TagResource>();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link.Name);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    siteContent = streamReader.ReadToEnd();
                }

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.OptionWriteEmptyNodes = true;
                doc.LoadHtml(siteContent);
                if (doc == null) return null;
                string output = "";
                foreach (var node in doc.DocumentNode.ChildNodes)
                {
                    output += node.InnerText;
                }

                var result = output.RemoveStopWords("en");
                result = result.RemoveSpecialCharacters();
                string[] splitkeywords = Regex.Split(result, "(?<=[a-z])(?=[A-Z])|(?<=[0-9])(?=[A-Za-z])|(?<=[A-Za-z])(?=[0-9])|(?<=\\W)(?=\\W)");
                splitkeywords = (from x in splitkeywords select x.Trim()).ToArray();

                var groupedkeywords = splitkeywords.GroupBy(split => split)
                    .Select(g => new { Keyword = g.Key, Count = g.Count() });

                var sortedkeywords = groupedkeywords.OrderByDescending(x => x.Count).ThenBy(X => X.Keyword).ToList();

                var count = sortedkeywords.Count() > 10 ? 10 : sortedkeywords.Count() - 1;

                foreach (var item in sortedkeywords.GetRange(0, count)) //temporary
                {
                    if (item.Count > 2)
                    {
                        for (int i = 0; i < item.Count; i++)
                        {
                            tagResourcesForSuggestion.Add(new TagResource { Name = item.Keyword, LinkId = link.ID });
                        }
                        //tagResourcesForSuggestion.Add(new TagResource { Name = item.Keyword, NumberOfOccurances = "Occured " + item.Count + " times.", LinkId = link.ID });
                    }
                    
                }

                return tagResourcesForSuggestion;
            }
            catch (Exception)
            {
                return null; //not possible to parse the provided URL
            }
        }
        public async Task<ActionResult> InitializeTags([Bind("Name, TagResources")] LinkResource model)
        {

            //if exists
            var linkOccurance = await _linkService.CheckLinkForOccurance(model.Name);
            if (linkOccurance != null)
            {
                var tagsWithOccurances = await _tagService.GetTagsByOccurancesAndLinkIdAsync(linkOccurance.ID);
                if (tagsWithOccurances != null && tagsWithOccurances.Any())
                {
                    model.TagResources = new List<TagResource>();
                    foreach (var item in tagsWithOccurances)
                    {
                        if (item.Item2 == 1)
                            model.TagResources.Add(new TagResource { Name = item.Item1, NumberOfOccurances = "Occured once.", LinkId = model.ID });
                        else
                            model.TagResources.Add(new TagResource { Name = item.Item1, NumberOfOccurances = "Occured " + item.Item2 + " times.", LinkId = model.ID });
                    }
                }
            }
            else
            {
                //suggest from content
                var contentTags = StripHtml(model);
                if (contentTags != null && contentTags.Any())
                {
                    var grouped = contentTags.GroupBy(x => x.Name)//DRY
                    .OrderBy(group => group.Key)
                    .Select(group => Tuple.Create(group.Key, group.Count()))
                    .ToList();

                    var sorted = grouped.OrderByDescending(x => x.Item2).ThenBy(X => X.Item1).ToList();

                    foreach (var item in sorted)
                    {
                        //item.LinkId = model.ID;
                        model.TagResources.Add(new TagResource { Name = item.Item1, NumberOfOccurances = "Occured " + item.Item2 + " times.", LinkId = model.ID });
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
        public ActionResult Logout()
        {
            Settings.CurrentUserId = String.Empty;
            Settings.CurrentUserName = String.Empty;
            return View("Login");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
