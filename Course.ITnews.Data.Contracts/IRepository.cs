using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Course.ITnews.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T Get(string id);
        void Add(T entity);
        void Update(T entity);
        void Remove(string id);
        void SaveChanges();
    }
}
