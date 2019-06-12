using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Autofac;
using Course.ITnews.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Course.ITnews.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IComponentContext componentContext;

        public UnitOfWork(ApplicationDbContext dbContext, IComponentContext componentContext)
        {
            this.dbContext = dbContext;
            this.componentContext = componentContext;
        }

        public void Add<T>(T entity) where T : class
        {
            var repository = GetRepository<T>();
            repository.Add(entity);
        }

        public IEnumerable<T> FindByCondition<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var repository = GetRepository<T>();
            var result = repository.FindByCondition(expression);
            return result;
        }

        public T Get<T>(int id) where T : class
        {
            var repository = GetRepository<T>();
            var result = repository.Get(id);
            return result;
        }

        public void Remove<T>(int id) where T : class
        {
            var repository = GetRepository<T>();
            repository.Remove(id);
        }

        public void Update<T>(T entity) where T : class
        {
            var repository = GetRepository<T>();
            repository.Update(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            var repository = GetRepository<T>();
            var result = repository.GetAll();
            return result;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var repository = componentContext.Resolve<IRepository<T>>();
            return repository;
        }
    }
}
