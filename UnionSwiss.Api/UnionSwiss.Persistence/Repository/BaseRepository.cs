using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnionSwiss.Domain.Common;
using UnionSwiss.Domain.Model.Entity.Interface;
using UnionSwiss.Domain.Repository;
using UnionSwiss.Persistence.Context;
using UnionSwiss.Persistence.Factory;

namespace UnionSwiss.Persistence.Repository
{
    public abstract class BaseRepository<TContext, TEntityType> : Logable, IRepository<TEntityType>
        where TContext : IBaseContext
    {
        private readonly IDbContextFactory<TContext> _dbContextFactory;
        private TContext _context;

        protected BaseRepository(IDbContextFactory<TContext> dbContextFactory)
        {
            Guard.ArgumentNotNull(dbContextFactory, "dbContextFactory");
            _dbContextFactory = dbContextFactory;
        }

        protected virtual TContext GetContext()
        {
            if (_context == null)
                _context = _dbContextFactory.CreateContext();

            return _context;
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>
        {
       

                Guard.ArgumentNotNull(predicate, "predicate");

                var result = GetContext().GetDbSet<T>().FirstOrDefault(predicate);

                return result;

        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>
        {

            Guard.ArgumentNotNull(predicate, "predicate");

            return GetContext().GetDbSet<T>().Where(predicate);

        }

        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>
        {
                Guard.ArgumentNotNull(predicate, "predicate");

                var context = GetContext();
                var found = context.GetDbSet<T>().Where(predicate).ToList();

                context.GetDbSet<T>().RemoveRange(found);

        }

        public T Save<T>(T entity, Func<bool> predicate) where T : class, IEntity<TEntityType>
        {

                Guard.ArgumentNotNull(entity, "entity");

                T savedEntity;

                var context = GetContext();

                if (predicate())
                {
                    savedEntity = context.GetDbSet<T>().Add(entity);
                }
                else
                {
                    savedEntity = context.GetDbSet<T>().Attach(entity);
                    context.SetModified(savedEntity);
                }

                context.Save();

                return savedEntity;

        }


        public IQueryable<T> All<T>() where T : class, IEntity<TEntityType>
        {
            return GetContext().GetDbSet<T>();
        }

        public bool Exists<T>(T entity) where T : class
        {
            return GetContext().GetDbSet<T>().Local.Any(e => e == entity);
        }


    }
}
