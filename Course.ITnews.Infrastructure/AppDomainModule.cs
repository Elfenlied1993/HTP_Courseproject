using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Services;

namespace Course.ITnews.Infrastructure
{
    public class AppDomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommentaryService>()
                .As<ICommentaryService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<NewsService>()
                .As<INewsService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();
        }
    }
}
