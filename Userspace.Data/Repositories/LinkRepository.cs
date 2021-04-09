using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Userspace.Core.Models;
using Userspace.Core.Models.Auth;
using Userspace.Core.Repositories;

namespace Userspace.Data.Repositories
{
    public class LinkRepository : Repository<Link>, ILinkRepository
    {
        public LinkRepository(UserspaceDbContext context)
            : base(context)
        {}

        public async Task<IEnumerable<Link>> GetAllLinksAsync(string userId)
        {
            return await UserspaceDbContext.UserLinks
                .Include(x => x.Link)
                .Where(x => x.UserId.ToString() == userId)
                .Select(x=>x.Link)
                .ToListAsync();
        }

        public async Task<UserLink> GetLinkByIdAsync(int linkId, string userId)
        {
            return await UserspaceDbContext.UserLinks
                .Include(x => x.Link)
                .Where(x => x.LinkId == linkId)
                .Where(x => x.UserId.ToString() == userId)
                    .SingleOrDefaultAsync();
        }
        public async Task<Link> CheckForLinkOccuranceAsync(string name)
        {
            string string1 = System.Web.HttpUtility.UrlDecode(name);
            if (string1.StartsWith("http://"))
                string1 = string1.Remove(0, 7);

            string[] words1 = string1.Split(new char[] { ',', '?', '=', '&' });
            HashSet<string> mySet1 = new HashSet<string>(words1.ToList());

            foreach (var link in UserspaceDbContext.Links)
            {
                if (link.Name.StartsWith("http://"))
                    link.Name = link.Name.Remove(0, 7);

                string[] words2 = link.Name.Split(new char[] { ',', '?', '=', '&' });
                HashSet<string> mySet2 = new HashSet<string>(words2.ToList());
                HashSet<string> mySetTemp = new HashSet<string>(mySet1);
                mySet1.ExceptWith(mySet2);
                if (!mySet1.Any())
                {
                    link.Name = link.Name.Insert(0, "http://");
                    return link;
                }
                else
                {
                    mySet1 = new HashSet<string>(mySetTemp);
                }
            }
            return null;
        }
        public async Task<IEnumerable<UserLink>> GetAllWithTagsAsync(string userId)
        {
            return await UserspaceDbContext.UserLinks
                .Include(x => x.Link)
                .Include(x => x.Tag)
                .Where(x => x.UserId.ToString() == userId)
                .ToListAsync();
        }
        public async Task<UserLink> GetWithTagsByIdAsync(int id, string userId)
        {
            return await UserspaceDbContext.UserLinks
               .Include(x => x.Tag)
               .Where(x => x.LinkId == id)
               .Where(x => x.UserId.ToString() == userId)
               .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<UserLink>> GetLinksByUserIdAsync(string userId)
        {
            return await UserspaceDbContext.UserLinks
                .Include(x => x.Link)
                .Where(x => x.UserId.ToString() == userId)
                .ToListAsync();
        }

        private UserspaceDbContext UserspaceDbContext
        {
            get { return Context as UserspaceDbContext; }
        }
    }
}
