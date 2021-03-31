using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models;

namespace Userspace.Web.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetTags();
        Task<TagViewModel> GetTagById(int id);
        Task<TagViewModel> CreateTag(TagViewModel link);
        Task<IEnumerable<TagViewModel>> GetTagsByLinkId(int linkId);
    }
}
