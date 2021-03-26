using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Userspace.Core.Models;
using Userspace.Data.Configurations;

namespace Userspace.Data
{
    public class UserspaceDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LinkTag> LinkTags { get; set; }

        public UserspaceDbContext(DbContextOptions<UserspaceDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new LinkConfiguration());

            builder
                .ApplyConfiguration(new TagConfiguration());

            builder
                .ApplyConfiguration(new LinkTagConfiguration());
        }
    }
}
