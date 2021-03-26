﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                    .SingleOrDefaultAsync();
        }
        private UserspaceDbContext UserspaceDbContext
        {
            get { return Context as UserspaceDbContext; }
        }
    }
}
