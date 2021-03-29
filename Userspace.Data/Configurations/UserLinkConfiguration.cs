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
               .HasKey(x => new { x.LinkId, x.UserId });
        }
    }
}
