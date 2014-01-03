using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sordid.Core.Interfaces
{
    public interface IBaseRepository<T>
        where T : class
    {
        Task<T> Get(int id);
        Task<IList<T>> GetAll();
        Task<IList<T>> Find(Expression<Func<T, bool>> predicate);
        //IQueryable<T> GetQueryable();
        //Task<IList<U>> Query<U>(IQueryable<U> queryable);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        IUnitOfWork UnitOfWork { get; }
    }
}
