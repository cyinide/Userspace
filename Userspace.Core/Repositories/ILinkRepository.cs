using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;

namespace Userspace.Core.Repositories
{
    public interface ILinkRepository : IRepository<Link>
    {
        Task<IEnumerable<Link>> GetLinksAsync();
        Task<IEnumerable<Link>> GetAllWithTagsAsync();
        Task<Link> GetWithTagsByIdAsync(int id);
    }
}
