using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core;
using Userspace.Core.Models;
using Userspace.Core.Services;

namespace Userspace.Services.Services
{
    public class LinkService : ILinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LinkService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Link>> GetAllLinksAsync(string userId)
        {
            return await _unitOfWork.Links
                .GetAllLinksAsync(userId);
        }
        public async Task<Link> GetLinkById(int id)
        {
            return await _unitOfWork.Links
                .GetByIdAsync(id);
        }
        public async Task<Link> CheckForLinkOccuranceAsync(string name)
        {
            return await _unitOfWork.Links
                .CheckForLinkOccuranceAsync(name);
        }
        public async Task<Link> CreateLink(Link newLink)
        {
            await _unitOfWork.Links.AddAsync(newLink);
            await _unitOfWork.CommitAsync();
            return newLink;
        }
        public async Task<IEnumerable<UserLink>> GetAllWithTagsAsync(string userId)
        {
            return await _unitOfWork.Links
                .GetAllWithTagsAsync(userId);
        }
        public async Task<UserLink> GetWithTagsByIdAsync(int id, string userId)
        {
            return await _unitOfWork.Links
                .GetWithTagsByIdAsync(id, userId);
        }
        public async Task<IEnumerable<UserLink>> GetLinksByUserId(string userId)
        {
            return await _unitOfWork.Links
                .GetLinksByUserIdAsync(userId);
        }
    }
}
