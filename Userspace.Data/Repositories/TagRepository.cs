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
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(UserspaceDbContext context)
            : base(context)
        { }
        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await UserspaceDbContext.Tags
                .ToListAsync();
        }
        public async Task<Tag> GetTagByIdAsync(int id)
        {
            return await UserspaceDbContext.Tags
                .Where(x => x.ID == id)
                    .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Tag>> GetTagsByLinkIdAsync(int linkId)
        {
            return await UserspaceDbContext.Tags
                .Include(x => x.Link)
                .Where(x => x.LinkId == linkId)
                .ToListAsync();
        }
        public async Task<List<Tuple<string, int>>> GetTagsByOccurancesAndLinkIdAsync(int linkId)
        {
            var x = await UserspaceDbContext.Tags
                .Include(x => x.Link)
                .Where(x => x.LinkId == linkId)
                .GroupBy(x=>x.Name)
                .OrderBy(group=>group.Key)
                .Select(group=>Tuple.Create(group.Key,group.Count()))
                .ToListAsync();
            var sort = x.OrderByDescending(x => x.Item2).ThenBy(X => X.Item1).ToList();
            return sort;
        }
        private UserspaceDbContext UserspaceDbContext
        {
            get { return Context as UserspaceDbContext; }
        }
    }
}
