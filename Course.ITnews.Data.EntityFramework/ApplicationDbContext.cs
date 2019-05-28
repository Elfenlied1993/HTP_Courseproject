using Course.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.EntityFramework.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Course.ITnews.Data.EntityFramework
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Commentary> Commentaries { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CommentaryEntityConfigrutaion());
            modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NewsTagEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TagEntityConfiguration());
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
