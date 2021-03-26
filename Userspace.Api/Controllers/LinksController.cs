using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Userspace.Api.Resources;
using Userspace.Core.Models;
using Userspace.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Userspace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IMapper _mapper;

        public LinksController(ILinkService linkService, IMapper mapper)
        {
            this._mapper = mapper;
            this._linkService = linkService;
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
            //var validator = new SaveLinkResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveLinkResource);

            //  if (!validationResult.IsValid)
            //   return BadRequest(validationResult.Errors); 

            var linkToCreate = _mapper.Map<SaveLinkResource, Link>(saveLinkResource);
            var newLink = await _linkService.CreateLink(linkToCreate);

            var link = await _linkService.GetLinkById(newLink.ID);
            var linkResource = _mapper.Map<Link, LinkResource>(newLink);

            return CreatedAtRoute(nameof(GetLinkById), new { Id = linkResource.ID }, linkResource);
        }
    }
}
