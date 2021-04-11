using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;

namespace Userspace.Core.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAll();
        Task<Tag> GetTagById(int id);
        Task<Tag> CreateTag(Tag newTag);
        Task<IEnumerable<Tag>> GetTagsByLinkId(int id);
        Task<List<Tuple<int, string, int>>> GetTagsByOccurancesAndLinkId(int linkId, string userId);
    }
}
