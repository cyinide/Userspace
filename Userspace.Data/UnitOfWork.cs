using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core;
using Userspace.Core.Repositories;
using Userspace.Data.Repositories;

namespace Userspace.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserspaceDbContext _context;
        private LinkRepository _linkRepository;
        private TagRepository _tagRepository;
        public UnitOfWork(UserspaceDbContext context)
        {
            this._context = context;
        }
        public ILinkRepistory Links => _linkRepository = _linkRepository ?? new LinkRepository(_context);
        public ITagRepository Tags => _tagRepository = _tagRepository ?? new TagRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
