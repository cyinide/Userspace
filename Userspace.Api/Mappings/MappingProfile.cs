using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Api.Resources;
using Userspace.Core.Models;

namespace Userspace.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Link, LinkResource>();
            CreateMap<Tag, TagResource>();
            CreateMap<Link, SaveLinkResource>();
            CreateMap<Tag, SaveTagResource>();

            CreateMap<LinkResource, Link>();
            CreateMap<TagResource, Tag>();
            CreateMap<SaveLinkResource, Link>();
            CreateMap<SaveTagResource, Tag>();
        }
    }
}
