using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Userspace.Core.Models;

namespace Userspace.Data.Configurations
{
    public class LinkTagConfiguration : IEntityTypeConfiguration<LinkTag>
    {
        public void Configure(EntityTypeBuilder<LinkTag> builder)
        {
            builder
               .HasKey(x => new { x.LinkId, x.TagId });
        }
    }
}
