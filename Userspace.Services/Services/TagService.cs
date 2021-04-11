using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core;
using Userspace.Core.Models;
using Userspace.Core.Services;

namespace Userspace.Services.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }    
        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _unitOfWork.Tags
                .GetAllAsync();
        }
        public async Task<Tag> GetTagById(int id)
        {
            return await _unitOfWork.Tags
               .GetByIdAsync(id);
        }
        public async Task<Tag> CreateTag(Tag newTag)
        {
            await _unitOfWork.Tags.AddAsync(newTag);
            await _unitOfWork.CommitAsync();
            return newTag;
        }
        public async Task<IEnumerable<Tag>> GetTagsByLinkId(int id, string userId)
        {
            return await _unitOfWork.Tags
                .GetTagsByLinkIdAsync(id, userId);
        }
        public async Task<List<Tuple<int, string, int>>> GetTagsByOccurancesAndLinkId(int linkId, string userId)
        {
            return await _unitOfWork.Tags
                .GetTagsByOccurancesAndLinkIdAsync(linkId, userId);
        }
        public async Task<Tag> CheckForTagOccurance(string url, string tagname, string userId)
        {
            return await _unitOfWork.Tags
              .CheckForTagOccuranceAsync(url, tagname, userId);
        }
    }
}
