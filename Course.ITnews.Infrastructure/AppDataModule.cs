using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Course.ITnews.Data.Contracts;
using Course.ITnews.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Course.ITnews.Infrastructure
{
    public class AppDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>()
                .As<ApplicationDbContext>()
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }
    }
}
