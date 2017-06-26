using System;
using System.Data.Entity;

namespace UnionSwiss.Persistence.Context
{
    public interface IBaseContext : IDisposable
    {
        DbSet<T> GetDbSet<T>() where T : class;
        void Save();
        void SetModified<T>(T entity) where T : class;

        void SetState<T>(T entity, EntityState state) where T : class;
        void SetValues<T>(T entity, T newEntity) where T : class;

    }
}
