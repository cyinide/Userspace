using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;

namespace Userspace.Core.Services
{
    public interface IUserLinkService
    {
        Task<UserLink> CreateUserLink(UserLink newLink);
    }
}
