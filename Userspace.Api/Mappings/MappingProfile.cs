using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Api.Resources;
using Userspace.Api.Resources.Auth;
using Userspace.Core.Models;
using Userspace.Core.Models.Auth;

namespace Userspace.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SignUpResource, User>();
            CreateMap<SignInResource, User>();
            CreateMap<Link, LinkResource>();
            //    .ForMember(x => x.TagResources, opt => opt.MapFrom(y => y.Tags));

            CreateMap<Tag, TagResource>();
            CreateMap<Link, SaveLinkResource>();
            CreateMap<Tag, SaveTagResource>();
            CreateMap<LinkResource, Link>();
            CreateMap<TagResource, Tag>();

            CreateMap<SaveLinkResource, Link>();
            CreateMap<SaveLinkResource, Tag>()
                  .ForMember(x => x.Name, opt => opt.MapFrom(y => y.SelectedTag));
            CreateMap<SaveTagResource, Tag>();
            CreateMap<UserLink, UserLinkResource>();
        }
    }
}
