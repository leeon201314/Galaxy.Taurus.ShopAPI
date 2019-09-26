using Galaxy.Taurus.IDBUtil;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxy.Taurus.DBUtil
{
    public class BaseContext<T> : DbContext, IBaseContext<T> where T : class
    {
        protected DbSet<T> CurrentDbSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conStr = new DBConnectionProvider().GetConnectionString();
            optionsBuilder.UseMySQL(conStr);
        }

        public virtual int Add(T t)
        {
            CurrentDbSet.Add(t);
            return SaveChanges();
        }

        public virtual void Update(T t)
        {
            CurrentDbSet.Update(t);
            SaveChanges();
        }

        public virtual void DeleteSingle(Expression<Func<T, bool>> predicate)
        {
            T t = SingleOrDefault(predicate);
            CurrentDbSet.Remove(t);
            SaveChanges();
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return CurrentDbSet.SingleOrDefault(predicate);
        }

        public virtual Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return CurrentDbSet.SingleOrDefaultAsync(predicate);
        }


        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return CurrentDbSet.FirstOrDefault(predicate);
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return CurrentDbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual List<T> List(Expression<Func<T, bool>> predicate)
        {
            return CurrentDbSet.Where(predicate)?.ToList();
        }

        public virtual Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return CurrentDbSet.Where(predicate)?.ToListAsync();
        }

        public virtual List<T> GetPage<TKey>(Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total)
        {
            total = CurrentDbSet.Where(where).Count();
            var list = CurrentDbSet.Where(where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return list.ToList();
        }

        public virtual Task<List<T>> GetPageAsync<TKey>(Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total)
        {
            total = CurrentDbSet.Where(where).Count();
            var list = CurrentDbSet.Where(where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return list.ToListAsync();
        }

        public virtual List<T> All()
        {
            return CurrentDbSet.ToList<T>();
        }
    }
}
