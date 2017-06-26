using System;
using System.Linq;
using System.Linq.Expressions;
using UnionSwiss.Domain.Model.Entity;
using UnionSwiss.Domain.Model.Entity.Interface;

namespace UnionSwiss.Domain.Repository
{
    public interface IRepository<TEntityType>
    {
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>;

        void Delete<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>;

  
        T Save<T>(T entity, Func<bool> predicate) where T : class, IEntity<TEntityType>;

        IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity<TEntityType>;
    }
}
