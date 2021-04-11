using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;

namespace Userspace.Core.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task<IEnumerable<Tag>> GetTagsByLinkIdAsync(int id, string userId);
        Task<List<Tuple<int, string, int>>> GetTagsByOccurancesAndLinkIdAsync(int linkId, string userId);
        Task<Tag> CheckForTagOccuranceAsync(string url,string tagname, string userId);
    }
}
