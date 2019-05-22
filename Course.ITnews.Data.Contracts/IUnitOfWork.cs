using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        T Get<T>(int id) where T : class;
        void Add<T>(T entity) where T : class;
        void Remove<T>(int id) where T : class;

    }
}
