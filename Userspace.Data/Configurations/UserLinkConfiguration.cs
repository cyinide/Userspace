using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Userspace.Core.Models;

namespace Userspace.Data.Configurations
{
    public class UserLinkConfiguration : IEntityTypeConfiguration<UserLink>
    {
        public void Configure(EntityTypeBuilder<UserLink> builder)
        {
            builder
               .HasKey(x => new { x.LinkId, x.UserId, x.TagId });

            builder
                 .HasOne(x => x.Link)
                 .WithMany(x => x.UserLinks)
                 .HasForeignKey(x => x.LinkId);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserLinks)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.Tag)
                .WithMany(x => x.UserLinks)
                .HasForeignKey(x => x.TagId);
        }
    }
}
