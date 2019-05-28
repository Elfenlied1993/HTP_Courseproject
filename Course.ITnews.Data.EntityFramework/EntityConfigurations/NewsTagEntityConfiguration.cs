using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class NewsTagEntityConfiguration : IEntityTypeConfiguration<NewsTag>
    {
        public void Configure(EntityTypeBuilder<NewsTag> builder)
        {
            builder.HasKey(x => new {x.NewsId, x.TagId});
            builder.HasOne(x => x.News).WithMany(x => x.NewsTags).HasForeignKey(x => x.NewsId);
            builder.HasOne(x => x.Tag).WithMany(x => x.NewsTags).HasForeignKey(x => x.TagId);
            builder.Ignore(x => x.Id);
        }
    }
}
