using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ITnews.Data.EntityFramework.EntityConfigurations
{
    class LikeEntityConfiguration:IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author).WithMany(x => x.Likes).HasForeignKey(x => x.AuthorId);
            builder.HasOne(x => x.Comment).WithMany(x => x.Likes).HasForeignKey(x => x.CommentId);
        }
    }
}
