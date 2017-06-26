using System.Data.Entity;
using UnionSwiss.Persistence.Context;

namespace UnionSwiss.Persistence.Factory
{
    public class DbContextFactory<T> : IDbContextFactory<T> where T : DbContext, IBaseContext, new()
    {
        public DbContextFactory()
        {
            Database.SetInitializer<T>(null);
        }

        public T CreateContext()
        {
            return new T();
        }
    }
}