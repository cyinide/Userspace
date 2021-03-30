using System;
using System.Collections.Generic;
using System.Text;
using Userspace.Core.Models;
using Userspace.Core.Repositories;

namespace Userspace.Data.Repositories
{
    public class UserLinkRepository : Repository<UserLink>, IUserLinkRepository
    {
        public UserLinkRepository(UserspaceDbContext context)
          : base(context)
        { }
        private UserspaceDbContext UserspaceDbContext
        {
            get { return Context as UserspaceDbContext; }
        }
    }
}
