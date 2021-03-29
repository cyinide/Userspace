using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Userspace.Core.Models;

namespace Userspace.Data.Configurations
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder
                .HasKey(m => m.ID);

            builder
                .Property(m => m.Name)
                .IsRequired();

            builder
            .HasMany(c => c.Tags)
            .WithOne(e => e.Link)
            .IsRequired();
        }
    }
}
