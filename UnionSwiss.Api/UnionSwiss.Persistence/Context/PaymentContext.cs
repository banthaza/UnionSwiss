using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using log4net;

namespace Zapper.Domain.Persistence.Context
{
    public class PaymentContext : DbContext, IPaymentContext
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PaymentContext() : base("ZapperContext")
        {
            Log.Debug(Database.Connection.ConnectionString);
            Log.Debug(this.Database.Connection.ConnectionString);
            base.Configuration.ProxyCreationEnabled = false;

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

        public void SetValues<T>(T entity, T newEntity) where T : class
        {

        }
    }
}
