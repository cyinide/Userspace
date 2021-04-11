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
                //.Include(x => x.Link)
                //.Where(x => x.LinkId == linkId)
                .ToListAsync();
        }
        public async Task<List<Tuple<int, string, int>>> GetTagsByOccurancesAndLinkIdAsync(int linkId, string userId)
        {
            var x = await UserspaceDbContext.UserLinks
                .Include(x => x.Tag)
                .Where(x=>x.LinkId == linkId)
                .Where(x => x.UserId.ToString() != userId)
                .GroupBy(x => new { x.Tag.ID, x.Tag.Name})
                .OrderBy(group=>group.Key.Name)
                .Select(group=>Tuple.Create(group.Key.ID, group.Key.Name, group.Count()))
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
