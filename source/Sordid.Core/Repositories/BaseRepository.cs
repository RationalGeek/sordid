using Ninject.Extensions.Logging;
using Sordid.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sordid.Core.Repositories
{
    // Good article on generic implementation of EF Code First repo pattern
    // http://www.codeproject.com/Articles/207820/The-Repository-Pattern-with-EF-code-first-Dependen

    // Reference articles for async support
    // http://entityframework.codeplex.com/wikipage?title=Task-based%20Asynchronous%20Pattern%20support%20in%20EF.
    // http://www.asp.net/mvc/tutorials/mvc-4/using-asynchronous-methods-in-aspnet-mvc-4

    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class, IIdKeyedEntity
    {
        protected SordidDbContext DbContext { get; private set; }
        public ILogger _Log { get; set; }
        public IUnitOfWork UnitOfWork { get; private set; }
        protected IList<Expression<Func<T, object>>> DefaultIncludes { get; private set; }
        protected DbSet<T> DbSet { get { return DbContext.Set<T>(); } }

        protected BaseRepository(IUnitOfWork uow, ILogger logger)
        {
            UnitOfWork = uow;
            DbContext = uow.Context;
            //_Log = new NullLogger();
            _Log = logger;
            DefaultIncludes = new List<Expression<Func<T, object>>>();
        }

        public virtual async Task<T> Get(int id)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.Id == id);            
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<IList<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public virtual IQueryable<T> GetQueryable()
        {
            QuickLog("Returning queryable for {0}");
            return DbSet;
        }

        public virtual async Task<IList<U>> Query<U>(IQueryable<U> queryable)
        {
            QuickLog("Querying {0}.  Query is: \n" + queryable.ToString());

            // Attempt to cast the queryable to a DbQuery so that we can execute it async
            var dbquery = queryable as DbQuery<U>;
            if (dbquery == null)
                throw new NotSupportedException("Queryable must be DbQuery.  Did you pass an IQueryable that was returned from GetQueryable()?");

            return await dbquery.ToListAsync<U>();
        }

        public virtual T Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            QuickLog("Added {0}");
            return entity;
        }

        public virtual T Update(T entity)
        {
            DbSet.Attach(entity);
            QuickLog("Updating {0} id {1}", entity);
            var entry = DbContext.Entry<T>(entity);
            entry.State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete(T entity)
        {
            QuickLog("Deleting {0} id {1}", entity);
            DbContext.Set<T>().Remove(entity);
        }

        private void QuickLog(string message, T entity = null)
        {
            if (_Log.IsDebugEnabled) // No need for reflection if we won't log
            {
                if (entity != null)
                    _Log.Debug(message, typeof(T).Name, GetEntityKey(entity));
                else
                    _Log.Debug(message, typeof(T).Name);
            }
        }

        private object GetEntityKey(T entity)
        {
            // NOTE: This will not work right if entity has multi-column key
            var objectStateEntry = ((IObjectContextAdapter)DbContext).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}
