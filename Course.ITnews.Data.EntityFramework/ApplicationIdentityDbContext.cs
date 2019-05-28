using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Data.EntityFramework.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Course.ITnews.Data.EntityFramework
{
    public class ApplicationIdentityDbContext : IdentityDbContext<User>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}