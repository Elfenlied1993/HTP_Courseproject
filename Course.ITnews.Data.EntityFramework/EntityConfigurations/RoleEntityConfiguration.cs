using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasData(new Role {Id = 1, Name = "Admin"},
                            new Role {Id = 2, Name = "Writer"},
                            new Role {Id = 3, Name = "Reader"});
        }
    }
}
