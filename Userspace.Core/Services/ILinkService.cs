using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;

namespace Userspace.Core.Services
{
    public interface ILinkService
    {
        Task<IEnumerable<Link>> GetAll();
        Task<Link> GetLinkById(int id);
        Task<Link> CheckForLinkOccuranceAsync(string name);
        Task<Link> CreateLink(Link newLink);
        Task<IEnumerable<UserLink>> GetAllWithTagsAsync(string userId);
        Task<UserLink> GetWithTagsByIdAsync(int id, string userId);
        Task<IEnumerable<UserLink>> GetLinksByUserId(string userId);
    }
}
