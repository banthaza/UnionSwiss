using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.WebApi;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UnionSwiss.Domain.Calculators;
using UnionSwiss.Domain.Repository;
using UnionSwiss.Domain.Service;
using UnionSwiss.Persistence.Context;
using UnionSwiss.Persistence.Factory;
using UnionSwiss.Persistence.Repository;
using UnionSwiss.Service;
using UnionSwiss.Service.Calculators;
using UnionSwiss.Service.Services;

namespace UnionSwiss.Api
{
    public class Global : System.Web.HttpApplication
    {

        public ILog Log { get; set; }
        private static IContainer _container;
        protected void Application_Start()
        {
            Log = LogManager.GetLogger(GetType());
        

            var config = GlobalConfiguration.Configuration;
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.UseDataContractJsonSerializer = false;
     
            var settings = jsonFormatter.SerializerSettings;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat; 
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            _container = InitializeContainer(config);


            config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);


            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        private static IContainer InitializeContainer(HttpConfiguration config)
        {

            var builder = new ContainerBuilder();

            // Config
     
            // Db Context 
            builder.RegisterType<UnionSwissContext>().As<IUnionSwissContext>().InstancePerRequest();
            builder.RegisterType(typeof(DbContextFactory<UnionSwissContext>)).As(typeof(IDbContextFactory<IUnionSwissContext>)).SingleInstance();
            builder.RegisterType<LongRepositroy>().As<IRepository<long>>();
            //Services
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerRequest();
            builder.RegisterType<FinanceAdminService>().As<IFinanceAdminService>().InstancePerRequest();

            builder.RegisterType<IncomeCalculator>().As<IIncomeCalculator>().InstancePerRequest();
            builder.RegisterType<TaxYearGenerator>().As<ITaxYearGenerator>().InstancePerRequest();

            //Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var ioc = builder.Build();
            return ioc;
        }
    }
}