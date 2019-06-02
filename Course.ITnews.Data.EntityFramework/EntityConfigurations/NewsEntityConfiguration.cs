using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class NewsEntityConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author).WithMany(x => x.News).HasForeignKey(x=>x.AuthorId);
            builder.HasOne(x => x.Category).WithMany(x => x.News).HasForeignKey(x => x.CategoryId);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.FullDescription).IsRequired();
            builder.Property(x => x.ShortDescription).IsRequired();
        }
    }
}
