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
        public async Task<IEnumerable<Link>> GetAll()
        {
            return await _unitOfWork.Links
                .GetLinksAsync();
        }
        public async Task<Link> GetLinkById(int id)
        {
            return await _unitOfWork.Links
                .GetByIdAsync(id);
        }
        public async Task<Link> CreateLink(Link newLink)
        {
            await _unitOfWork.Links.AddAsync(newLink);
            await _unitOfWork.CommitAsync();
            return newLink;
        }
        public async Task<IEnumerable<Link>> GetAllWithTagsAsync()
        {
            return await _unitOfWork.Links
                .GetAllWithTagsAsync();
        }
        public async Task<Link> GetWithTagsByIdAsync(int id)
        {
            return await _unitOfWork.Links
                .GetWithTagsByIdAsync(id);
        }
    }
}
