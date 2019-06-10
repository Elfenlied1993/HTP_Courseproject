using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class RatingEntityConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.News).WithMany(x => x.Ratings).HasForeignKey(x => x.NewsId);
            builder.HasOne(x => x.Author).WithMany(x => x.Ratings).HasForeignKey(x => x.AuthorId);
        }
    }
}
