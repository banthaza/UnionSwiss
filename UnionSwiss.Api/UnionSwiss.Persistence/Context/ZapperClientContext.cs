using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using log4net;
using Zapper.Common.Constants;

using Zapper.Domain.Model.Entity.Interface;
using Zapper.Domain.Persistence.Constants;

namespace Zapper.Domain.Persistence.Context
{
    public class ZapperClientContext : DbContext, IZapperClientContext
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ZapperClientContext() : base("zapperConnectionString")
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

            //base.OnModelCreating(modelBuilder);
            var sqliteConnectionInitializer = new ZapperSqliteContextInitializer(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }

        public void Save()
        {
            SaveChanges();
        }

        public void SetModified<T>(T entity) where T : class, IEntity
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
