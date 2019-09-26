using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxy.Taurus.IDBUtil
{
    public interface IBaseContext<T> where T : class
    {
        int Add(T entity);

        void Update(T entity);

        void DeleteSingle(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        List<T> List(Expression<Func<T, bool>> predicate);

        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);

        List<T> All();

        List<T> GetPage<TKey>(Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total);

        Task<List<T>> GetPageAsync<TKey>(Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total);
    }
}
