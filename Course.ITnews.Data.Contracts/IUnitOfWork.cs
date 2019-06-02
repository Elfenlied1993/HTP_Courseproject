using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Course.ITnews.Data.Contracts
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        IEnumerable<T> FindByCondition<T>(Expression<Func<T, bool>> expression) where T :class;
        T Get<T>(string id) where T : class;
        void Add<T>(T entity) where T : class;
        void Remove<T>(string id) where T : class;
        void Update<T>(T entity) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
    }
}
