using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewPDR.Data;
using NewPDR.Data.Infrastructure;
using NewPDR.Data.Repository;
using NewPDR.Model;
using NewPDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NewPDR.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
           // AutoMapperConfiguration.Configure();
        }
        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
           
            builder.RegisterAssemblyTypes(typeof(UserTypeRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerRequest();

          //  builder.RegisterAssemblyTypes(typeof(Objectiverepository).Assembly)
          //.Where(t => t.Name.EndsWith("Repository"))
          //.AsImplementedInterfaces().InstancePerRequest();

          //  builder.RegisterAssemblyTypes(typeof(ObjectiveTypeRepository).Assembly)
          //.Where(t => t.Name.EndsWith("Repository"))
          //.AsImplementedInterfaces().InstancePerRequest();

          //  builder.RegisterAssemblyTypes(typeof(PDReviewRepository).Assembly)
          //.Where(t => t.Name.EndsWith("Repository"))
          //.AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces().InstancePerRequest();

        //    builder.RegisterAssemblyTypes(typeof(PDRService).Assembly)
        //.Where(t => t.Name.EndsWith("Service"))
        //.AsImplementedInterfaces().InstancePerRequest();

         //   builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
         //.Where(t => t.Name.EndsWith("Authentication"))
         //.AsImplementedInterfaces().InstancePerHttpRequest();

            builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new NewPDRDataContext ())))
                .As<UserManager<ApplicationUser>>().InstancePerRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}