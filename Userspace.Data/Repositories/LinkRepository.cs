using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Userspace.Core.Models;
using Userspace.Core.Repositories;

namespace Userspace.Data.Repositories
{
    public class LinkRepository : Repository<Link>, ILinkRepository
    {
        public LinkRepository(UserspaceDbContext context)
            : base(context)
        { }
        public async Task<IEnumerable<Link>> GetLinksAsync()
        {
            return await UserspaceDbContext.Links
                .ToListAsync();
        }
        public async Task<Link> GetLinkByIdAsync(int linkId)
        {
            return await UserspaceDbContext.Links
                .Where(x => x.ID == linkId)
                    .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Link>> GetAllWithTagsAsync()
        {
            return await UserspaceDbContext.Links
                .Include(x => x.Tags)
                .ToListAsync();
        }
        public async Task<Link> GetWithTagsByIdAsync(int id)
        {
            return await UserspaceDbContext.Links
               .Include(x => x.Tags)
               .Where(x => x.ID == id)
               .FirstOrDefaultAsync();
        }
        private UserspaceDbContext UserspaceDbContext
        {
            get { return Context as UserspaceDbContext; }
        }
    }
}
