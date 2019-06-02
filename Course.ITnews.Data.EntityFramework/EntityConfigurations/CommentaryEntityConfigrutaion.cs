using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class CommentaryEntityConfigrutaion : IEntityTypeConfiguration<Commentary>
    {
        public void Configure(EntityTypeBuilder<Commentary> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.News).WithMany(x => x.Commentaries).HasForeignKey(x => x.NewsId);
            builder.HasOne(x => x.Author).WithMany(x => x.Commentaries).HasForeignKey(x => x.AuthorId);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
