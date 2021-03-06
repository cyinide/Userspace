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
        Task<IEnumerable<TagResource>> GetTags();
        Task<TagViewModel> GetTagById(int id);
        Task<TagResource> CreateTag(TagResource tag);
        Task<IEnumerable<TagResource>> GetTagsByLinkId(int linkId);
        Task<TagResource> CheckTagForOccuranceAsync(string url, string tagname);
        Task<List<Tuple<int, string, int>>> GetTagsByOccurancesAndLinkIdAsync(int linkId);
    }
}
