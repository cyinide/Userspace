using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Userspace.Core.Models;

namespace Userspace.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .HasKey(m => m.ID);

            builder
                .Property(m => m.Name)
                .IsRequired();
        }
    }
}
