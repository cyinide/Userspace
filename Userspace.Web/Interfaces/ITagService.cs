using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models;
using Userspace.Web.Resources;

namespace Userspace.Web.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetTags();
        Task<TagViewModel> GetTagById(int id);
        Task<TagViewModel> CreateTag(TagViewModel link);
        Task<IEnumerable<TagResource>> GetTagsByLinkId(int linkId);
    }
}
