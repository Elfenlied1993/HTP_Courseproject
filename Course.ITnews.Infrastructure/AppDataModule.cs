using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Course.ITnews.Data.Contracts;
using Course.ITnews.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Course.ITnews.Infrastructure
{
    class AppDataModule : Module
    {
        public string ConnectionString { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
