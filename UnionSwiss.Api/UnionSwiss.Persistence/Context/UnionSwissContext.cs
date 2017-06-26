using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using log4net;
using UnionSwiss.Persistence.Model.Map;

namespace UnionSwiss.Persistence.Context
{
    public class UnionSwissContext : DbContext, IUnionSwissContext
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UnionSwissContext() : base("UnionSwissContext")
        {
            Log.Debug(Database.Connection.ConnectionString);
            Log.Debug(this.Database.Connection.ConnectionString);

            Database.Log = s =>
            {
                Debug.WriteLine(Database.Connection.ConnectionString);
                Debug.Write(s);
                Debug.WriteLine(string.Empty);
                Log.Debug(s);
            };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
    
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }

        public void Save()
        {
            SaveChanges();
        }

        public void SetModified<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void SetState<T>(T entity, EntityState state) where T : class
        {
            Entry(entity).State = state;
        }

        public void SetValues<T>(T entity, T newEntity) where T : class
        {
            Entry(entity).CurrentValues.SetValues(newEntity);
        }
    }
}
