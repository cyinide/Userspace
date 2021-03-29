using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models.Auth;

namespace Userspace.Web.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginViewModel login);
        Task<bool> Register(RegisterViewModel register);
    }
}
