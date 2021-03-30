using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core;
using Userspace.Core.Models;
using Userspace.Core.Services;

namespace Userspace.Services.Services
{
    public class UserLinkService : IUserLinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserLinkService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<UserLink> CreateUserLink(UserLink newuserLink)
        {
            await _unitOfWork.UserLinks.AddAsync(newuserLink);
            await _unitOfWork.CommitAsync();
            return newuserLink;
        }
    }
}
