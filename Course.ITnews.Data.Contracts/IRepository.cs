using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(int id);
        void SaveChanges();
    }
}
