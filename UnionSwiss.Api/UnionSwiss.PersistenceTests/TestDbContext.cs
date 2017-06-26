using System;
using System.Data.Entity;
using UnionSwiss.Persistence.Context;

namespace UnionSwiss.PersistenceTests
{
    public class TestDbContext : IBaseContext
    {
        public bool ValuesSet = false;

        public virtual DbSet<T> GetDbSet<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }

        public virtual void SetModified<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void SetState<T>(T entity, EntityState state) where T : class
        {
            throw new NotImplementedException();
        }

        public void SetValues<T>(T entity, T newEntity) where T : class
        {
            ValuesSet = true;
        }

        public void Dispose()
        {
            Console.WriteLine("Context Disposed...");
        }
    }
}