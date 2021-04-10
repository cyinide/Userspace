using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Userspace.Api.Resources;
using Userspace.Api.Validators;
using Userspace.Core.Models;
using Userspace.Core.Models.Auth;
using Userspace.Core.Services;
using static Userspace.Api.Settings;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Userspace.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly ITagService _tagService;
        private readonly IUserLinkService _userLinkService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public LinksController(ILinkService linkService, ITagService tagService, IUserLinkService userLinkService, UserManager<User> userManager, IMapper mapper)
        {
            this._mapper = mapper;
            this._linkService = linkService;
            this._tagService = tagService;
            this._userLinkService = userLinkService;
            this._userManager = userManager;
        }
        // GET: api/links
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<LinkResource>>> GetAll()
        {
            userId = _userManager.GetUserId(User); //temporary - in data layer with DI?

            var links = await _linkService.GetAllLinksAsync(userId);
            var linkResources = _mapper.Map<IEnumerable<Link>, IEnumerable<LinkResource>>(links);

            return Ok(linkResources);
        }
        // GET: api/links/id
        [HttpGet("{id}", Name = "GetLinkById")]
        public async Task<ActionResult<LinkResource>> GetLinkById(int id)
        {
            var link = await _linkService.GetLinkById(id);
            if (link == null)
                return NotFound();
            var linkResource = _mapper.Map<Link, LinkResource>(link);

            return Ok(linkResource);
        }
        // GET: api/links/name
        [HttpGet("checkforoccurance/{name}")]
        public async Task<ActionResult<LinkResource>> CheckForLinkOccuranceAsync(string name)
        {
            var link = await _linkService.CheckForLinkOccuranceAsync(name);
            var linkResource = _mapper.Map<Link, LinkResource>(link);

            return Ok(linkResource);
        }
        // POST: api/links
        [HttpPost("")]
        public async Task<ActionResult<SaveLinkResource>> CreateLink([FromBody] SaveLinkResource saveLinkResource)
        {
            //ClaimsPrincipal currentUser = this.User;
            //if (!currentUser.Claims.Any())
            //    return Unauthorized(); 
            //userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var validator = new SaveLinkResourceValidator();
            var validationResult = await validator.ValidateAsync(saveLinkResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var linkToCreate = _mapper.Map<SaveLinkResource, Link>(saveLinkResource);

            var existingLink = await _linkService.CheckForLinkOccuranceAsync(linkToCreate.Name);
            if (existingLink != null)
            {
                await _userLinkService.CreateUserLink(new UserLink { LinkId = existingLink.ID, UserId = Guid.Parse(userId)});
                //foreach (var item in linkToCreate.Tags)
                //{
                //    await _tagService.CreateTag(new Tag { LinkId = existingLink.ID, Name = item.Name }); // TODO: CreateTagRange
                //}
                return Conflict(new { message = $"An existing record with the id '{existingLink.ID}' was already found." });
            }
            var newLink = await _linkService.CreateLink(linkToCreate);
            await _userLinkService.CreateUserLink(new UserLink { LinkId = newLink.ID, UserId = Guid.Parse(userId) });
            var link = await _linkService.GetLinkById(newLink.ID);
            var linkResource = _mapper.Map<Link, LinkResource>(newLink);

            return CreatedAtRoute(nameof(GetLinkById), new { Id = newLink.ID }, linkResource);
        }
        // GET: api/links/withtags
        [HttpGet("withtags")]
        public async Task<ActionResult<IEnumerable<LinkResource>>> GetAllWithTags()
        {
            //var links = await _linkService.GetAllWithTagsAsync();
            //if (links == null)
            //    return NotFound();
            //var linkResources = _mapper.Map<IEnumerable<UserLink>, IEnumerable<LinkResource>>(links);

            return Ok(); //linkResources
        }
        // GET: api/links/withtags/id
        [HttpGet("withtagsbyid/{id}")]
        public async Task<ActionResult<LinkResource>> GetWithTagsById(int id)
        {
            //var link = await _linkService.GetWithTagsByIdAsync(id);
            //if (link == null)
            //    return NotFound();
            //var linkResource = _mapper.Map<Link, LinkResource>(link);

            return Ok();//linkResource
        }
        // GET: api/links/withtagsbyuserid/id
        [HttpGet("withtagsbyuserid/{userId}")]
        public async Task<ActionResult<IEnumerable<UserLinkResource>>> GetAllWithTagsByUserId(string userId)
        {
            var userLinks = await _linkService.GetLinksByUserId(userId);
            if (userLinks == null)
                return NotFound();
            var userLinkResources = _mapper.Map<IEnumerable<UserLink>, IEnumerable<UserLinkResource>>(userLinks);

            return Ok(userLinkResources);
        }
    }
}
