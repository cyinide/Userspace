using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Repositories;

namespace Userspace.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ILinkRepository Links { get; }
        ITagRepository Tags { get; }
        IUserLinkRepository UserLinks { get; }
        Task<int> CommitAsync();
    }
}
