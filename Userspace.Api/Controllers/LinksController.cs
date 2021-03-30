using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Userspace.Api.Resources;
using Userspace.Api.Validators;
using Userspace.Core.Models;
using Userspace.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Userspace.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IUserLinkService _userLinkService;
        private readonly IMapper _mapper;
        public LinksController(ILinkService linkService, IUserLinkService userLinkService, IMapper mapper)
        {
            this._mapper = mapper;
            this._linkService = linkService;
            this._userLinkService = userLinkService;
        }
        // GET: api/links
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<LinkResource>>> GetAll()
        {
            var links = await _linkService.GetAll();
            var linkResources = _mapper.Map<IEnumerable<Link>, IEnumerable<LinkResource>>(links);

            return Ok(linkResources);
        }
        // GET: api/links/id
        [HttpGet("{id}", Name = "GetLinkById")]
        public async Task<ActionResult<LinkResource>> GetLinkById(int id)
        {
            var link = await _linkService.GetLinkById(id);
            var linkResource = _mapper.Map<Link, LinkResource>(link);

            return Ok(linkResource);
        }
        // POST: api/links
        [HttpPost("")]
        public async Task<ActionResult<SaveLinkResource>> CreateLink([FromBody] SaveLinkResource saveLinkResource)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            if(String.IsNullOrEmpty(currentUserID))
                return Unauthorized();

            var validator = new SaveLinkResourceValidator();
            var validationResult = await validator.ValidateAsync(saveLinkResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var linkToCreate = _mapper.Map<SaveLinkResource, Link>(saveLinkResource);
            var newLink = await _linkService.CreateLink(linkToCreate);

            await _userLinkService.CreateUserLink(new UserLink { LinkId = newLink.ID, UserId = Guid.Parse(currentUserID) });

            var link = await _linkService.GetLinkById(newLink.ID);
            var linkResource = _mapper.Map<Link, LinkResource>(newLink);

            return CreatedAtRoute(nameof(GetLinkById), new { Id = linkResource.ID }, linkResource);
        }
        // GET: api/links/withtags
        [HttpGet("withtags")]
        public async Task<ActionResult<IEnumerable<LinkResource>>> GetAllWithTags()
        {
            var links = await _linkService.GetAllWithTagsAsync();
            var linkResources = _mapper.Map<IEnumerable<Link>, IEnumerable<LinkResource>>(links);

            return Ok(linkResources);
        }
        // GET: api/links/withtags/id
        [HttpGet("withtagsbyid/{id}")]
        public async Task<ActionResult<LinkResource>> GetWithTagsById(int id)
        {
            var link = await _linkService.GetWithTagsByIdAsync(id);
            var linkResource = _mapper.Map<Link, LinkResource>(link);

            return Ok(linkResource);
        }
        // GET: api/links/withtagsbyuserid/id
        [HttpGet("withtagsbyuserid/{userId}")]
        public async Task<ActionResult<IEnumerable<UserLinkResource>>> GetAllWithTagsByUserId(string userId)
        {
            var userLinks = await _linkService.GetLinksByUserId(userId);
            var userLinkResources = _mapper.Map<IEnumerable<UserLink>, IEnumerable<UserLinkResource>>(userLinks);

            return Ok(userLinkResources);
        }
    }
}
