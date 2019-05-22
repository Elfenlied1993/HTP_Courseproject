using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Data.Contracts;

namespace Course.ITnews.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
