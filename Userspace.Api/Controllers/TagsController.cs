using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Userspace.Api.Resources;
using Userspace.Api.Validators;
using Userspace.Core.Models;
using Userspace.Core.Services;
using static Userspace.Api.Settings;

namespace Userspace.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        public TagsController(ITagService tagService, IMapper mapper)
        {
            this._mapper = mapper;
            this._tagService = tagService;
        }
        // GET: api/tags
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TagResource>>> GetAll()
        {
            var tags = await _tagService.GetAllTagsAsync(userId);
            var tagResources = _mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);

            return Ok(tagResources);
        }
        // GET: api/tags/id
        [HttpGet("{id}", Name = "GetTagById")]
        public async Task<ActionResult<TagResource>> GetTagById(int id)
        {
            var tag = await _tagService.GetTagById(id);
            if (tag == null)
                return NotFound();
            var tagResource = _mapper.Map<Tag, TagResource>(tag);

            return Ok(tagResource);
        }
        // POST: api/tags
        [HttpPost("")]
        public async Task<ActionResult<SaveTagResource>> CreateTag([FromBody] SaveTagResource saveTagResource)
        {
            var validator = new SaveTagResourceValidator();
            var validationResult = await validator.ValidateAsync(saveTagResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var tagToCreate = _mapper.Map<SaveTagResource, Tag>(saveTagResource);
            var newTag = await _tagService.CreateTag(tagToCreate);
            var tag = await _tagService.GetTagById(newTag.ID);
            var tagResource = _mapper.Map<Tag, TagResource>(newTag);

            return CreatedAtRoute(nameof(GetTagById), new { Id = tagResource.ID }, tagResource);
        }
        // GET: api/tags/bylinkid
        [HttpGet("bylinkid/{linkId}")]
        public async Task<ActionResult<IEnumerable<LinkResource>>> GetTagsByLinkId(int linkId)
        {
            var tags = await _tagService.GetTagsByLinkId(linkId, userId);
            if (tags == null)
                return NotFound();
            var tagResources = _mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);

            return Ok(tagResources);
        }
        // GET: api/tags/gettagsbyOccurancesAndLinkid/linkId
        [HttpGet("gettagsbyOccurancesAndLinkid/{linkId}")]
        public async Task<ActionResult<List<Tuple<string, int>>>> GetTagsByOccurancesAndLinkIdAsync(int linkId)
        {
            var tags = await _tagService.GetTagsByOccurancesAndLinkId(linkId, userId);
            if (tags == null)
                return NotFound();

            return Ok(tags);
        }
        // GET: api/tags/checkforoccurance/name
        [HttpGet("checkforoccurance/{name}")]
        public async Task<ActionResult<LinkResource>> CheckForLinkOccuranceAsync(string name)
        {
            var tag = await _tagService.CheckForTagOccuranceAsync(name);
            var tagResource = _mapper.Map<Tag, TagResource>(tag);

            return Ok(tagResource);
        }
    }
}

