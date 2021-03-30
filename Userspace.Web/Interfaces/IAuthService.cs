using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models.Auth;

namespace Userspace.Web.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(LoginViewModel model);
        Task<bool> Register(RegisterViewModel model);
    }
}
