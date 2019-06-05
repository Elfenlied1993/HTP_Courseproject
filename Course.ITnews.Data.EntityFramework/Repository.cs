using Course.ITnews.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Course.ITnews.Data.EntityFramework
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(T entity)
        {
            var dbSet = dbContext.Set<T>();
            dbSet.Add(entity);
            SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where(expression).ToList();
        }

        public T Get(int id)
        {
            var dbSet = dbContext.Set<T>();
            var result = dbSet.Find(id);
            return result;
        }

        public void Remove(int id)
        {
            var dbSet = dbContext.Set<T>();
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.Set<T>().Update(entity);
            SaveChanges();
        }
    }
}
