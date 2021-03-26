using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;
using Userspace.Core.Repositories;

namespace Userspace.Data.Repositories
{
    public class LinkRepository : Repository<Link>, ILinkRepistory
    {
        public LinkRepository(UserspaceDbContext context)
            : base(context)
        { }
        public async Task<IEnumerable<Link>> GetLinksAsync()
        {
            return await UserspaceDbContext.Links
                .ToListAsync();
        }
        public async Task<Link> GetLinkByIdAsync(int id)
        {
            return await UserspaceDbContext.Links
                    .SingleOrDefaultAsync();
        }
        private UserspaceDbContext UserspaceDbContext
        {
            get { return Context as UserspaceDbContext; }
        }
    }
}
