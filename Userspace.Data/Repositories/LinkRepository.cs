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
        public async Task<Link> GetLinkByIdAsync(int linkId)
        {
            return await UserspaceDbContext.Links
                .Where(x => x.ID == linkId)
                    .SingleOrDefaultAsync();
        }
        //refactor based on DRY principles
        public async Task<Link> CheckForLinkOccuranceAsync(string name) 
        {
            string string1 = name.Replace("%2F","/");
            if (string1.StartsWith("http://"))
                string1 = string1.Remove(0, 7);

            string[] words1 = string1.Split(new char[] { ',', '?', '=', '&' });
            HashSet<string> mySet1 = new HashSet<string>(words1.ToList());

            foreach (var link in UserspaceDbContext.Links) 
            {
                if (link.Name.StartsWith("http://"))
                    link.Name = link.Name.Remove(0, 7); //just in case

                string[] words2 = link.Name.Split(new char[] { ',', '?', '=', '&' });
                HashSet<string> mySet2 = new HashSet<string>(words2.ToList());
                HashSet<string> mySetTemp = new HashSet<string>(mySet1);
                mySet1.ExceptWith(mySet2);
                if (!mySet1.Any())
                    return link;
                else
                {
                    mySet1 = new HashSet<string>(mySetTemp);
                }
            }
            return null;
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
        public async Task<IEnumerable<UserLink>> GetLinksByUserIdAsync(string userId)
        {
            return await UserspaceDbContext.UserLinks
                .Where(x => x.UserId == Guid.Parse(userId))
                .Include(x => x.Link)
                .ThenInclude(x => x.Tags)
                .ToListAsync();
        }
        private UserspaceDbContext UserspaceDbContext
        {
            get { return Context as UserspaceDbContext; }
        }
    }
}
