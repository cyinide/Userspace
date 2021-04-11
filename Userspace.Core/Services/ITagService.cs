using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;

namespace Userspace.Core.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync(string userId);
        Task<Tag> GetTagById(int id);
        Task<Tag> CreateTag(Tag newTag);
        Task<IEnumerable<Tag>> GetTagsByLinkId(int id, string userId);
        Task<List<Tuple<int, string, int>>> GetTagsByOccurancesAndLinkId(int linkId, string userId);
        Task<Tag> CheckForTagOccuranceAsync(string name);
    }
}
