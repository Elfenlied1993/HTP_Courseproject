using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Login).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x=>x.RoleId);
        }
    }
}
