﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zapper.Common;
using Zapper.Common.Constants;

using Zapper.Domain.Model.Entity.Interface;
using Zapper.Domain.Persistence.Context;
using Zapper.Domain.Persistence.Factory;
using Zapper.Domain.Repositories;

namespace Zapper.Domain.Persistence.Repository
{
    public abstract class TrackedBaseRepository<TContext, TEntityType> : Logable, IRepository<TEntityType>
        where TContext : IBaseContext
    {
        private readonly IDbContextFactory<TContext> _dbContextFactory;
        private TContext _context;

        protected TrackedBaseRepository(IDbContextFactory<TContext> dbContextFactory)
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
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(predicate, "predicate");

                var result = GetContext().GetDbSet<T>().FirstOrDefault(predicate);

                return result;
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public IList<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(predicate, "predicate");

                return GetContext().GetDbSet<T>().Where(predicate).ToList();
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public T FindSingle<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(predicate, "predicate");

                return GetContext().GetDbSet<T>().SingleOrDefault(predicate);
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public T FindFirst<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>
        {
            return GetContext().GetDbSet<T>().FirstOrDefault(predicate);
        }

        public T FindLast<T>() where T : class, IEntity<TEntityType>
        {
            return GetContext().GetDbSet<T>().OrderByDescending(t => t.Id).FirstOrDefault();
        }

        protected IQueryable<T> FindAll<T>() where T : class, IEntity<TEntityType>
        {
            return GetContext().GetDbSet<T>();
        }

        protected bool Exists<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>
        {
               var result= GetContext().GetDbSet<T>().Any(predicate);
                return result;
        }

        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(predicate, "predicate");

                var context = GetContext();
                var found = context.GetDbSet<T>().Where(predicate).ToList();

                context.GetDbSet<T>().RemoveRange(found);
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public Task SaveAsync<T>(T entity, Func<bool> predicate) where T : class, IEntity<TEntityType>
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(entity, "entity");

                return Task.Factory.StartNew(() => Save(entity, predicate));
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public T Create<T>(T entity) where T : class, IEntity<TEntityType>
        {
            Save(entity, () => true);
            return entity;
        }


        public T Save<T>(T entity, Func<bool> predicate) where T : class, IEntity<TEntityType>
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(entity, "entity");
                
                var context = GetContext();

                if (predicate())
                {
                   entity = context.GetDbSet<T>().Add(entity);
                }
                
                context.Save();

                return entity;
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public void Save()
        {
            var context = GetContext();
            context.Save();
        }


    }
}
