
using Autofac;
using Autofac.Integration.WebApi;
using NewPDR.Data;
using NewPDR.Data.Infrastructure;
using NewPDR.Data.Repository;
using NewPDR.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace NewPDR.WebAPI.App_Start
{
    public class AutofacWebapiConfig
    {

        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            builder.RegisterType<NewPDRDataContext>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DatabaseFactory>()
                .As<IDatabaseFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

          
            builder.RegisterAssemblyTypes(typeof(UserTypeRepository).Assembly)
          .Where(t => t.Name.EndsWith("Repository"))
          .AsImplementedInterfaces().InstancePerRequest();

          
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces().InstancePerRequest();


            Container = builder.Build();

            return Container;
        }

    }
}