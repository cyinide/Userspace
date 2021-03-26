using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Repositories;

namespace Userspace.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ILinkRepistory Links { get; }
        ITagRepository Tags { get; }
        Task<int> CommitAsync();
    }
}
