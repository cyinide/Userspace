using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Userspace.Core.Models;
using Userspace.Core.Models.Auth;
using Userspace.Data.Configurations;

namespace Userspace.Data
{
    public class UserspaceDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Link> Links { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserLink> UserLinks { get; set; } 

        public UserspaceDbContext(DbContextOptions<UserspaceDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new LinkConfiguration());
            builder
                .ApplyConfiguration(new TagConfiguration());
            builder
                .ApplyConfiguration(new UserLinkConfiguration());
        }
    }
}
