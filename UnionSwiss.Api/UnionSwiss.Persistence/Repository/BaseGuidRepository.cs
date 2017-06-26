using System;
using System.Collections.Generic;
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
    public abstract class BaseGuidRepository<TContext> : Logable, IGuidRepository
        where TContext : IBaseContext
    {
        private readonly IDbContextFactory<TContext> _dbContextFactory;
        private TContext _context;

 

        protected BaseGuidRepository(IDbContextFactory<TContext> dbContextFactory)
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

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
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

        public IList<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
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

        public T FindSingle<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
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


        public T FindLast<T>() where T : class, IGuidEntity
        {
            return GetContext().GetDbSet<T>().AsNoTracking().OrderByDescending(t => t.Id).FirstOrDefault();
        }

        protected IQueryable<T> FindAll<T>() where T : class, IEntity
        {
            return GetContext().GetDbSet<T>().AsNoTracking();
        }

        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
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

        public Task SaveAsync<T>(T entity) where T : class, IGuidEntity
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(entity, "entity");

                return Task.Factory.StartNew(() => Save(entity));
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public T Create<T>(T entity) where T : class, IEntity
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(entity, "entity");

                var savedEntity = GetContext().GetDbSet<T>().Add(entity);

                GetContext().Save();


                return savedEntity;
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public T Save<T>(T entity) where T : class, IGuidEntity
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(entity, "entity");

                T savedEntity;

                var context = GetContext();

                if (entity.Id == Guid.Empty)
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
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public IEnumerable<T> CreateRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                IEnumerable<T> savedEntities = savedEntities = GetContext().GetDbSet<T>().AddRange(entities);

                GetContext().Save();


                return savedEntities;
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }

        public T UpdateFull<T>(T entity) where T : class, IEntity
        {
            try
            {
                Log.Debug(LoggingConstants.Entering);

                Guard.ArgumentNotNull(entity, "entity");

                T savedEntity;

                savedEntity = GetContext().GetDbSet<T>().Attach(entity);

                GetContext().SetModified(entity);

                GetContext().Save();


                return savedEntity;
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
        }
    }
}
