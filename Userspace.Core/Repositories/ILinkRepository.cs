using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;

namespace Userspace.Core.Repositories
{
    public interface ILinkRepository : IRepository<Link>
    {
        Task<IEnumerable<Link>> GetAllLinksAsync(string name);
        Task<Link> CheckForLinkOccuranceAsync(string name);
        Task<IEnumerable<UserLink>> GetAllWithTagsAsync(string userId);
        Task<UserLink> GetWithTagsByIdAsync(int id, string userId);
        Task<IEnumerable<UserLink>> GetLinksByUserIdAsync(string userId);
    }
}
