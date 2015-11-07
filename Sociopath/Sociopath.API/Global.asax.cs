using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Sociopath.Data;
using Sociopath.Data.DataContext;
using Sociopath.DataContracts;

namespace Sociopath.API
{

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BuildContainer();
        }

        private void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SessionFactoryProvider>().As<ISessionFactoryProvider>().SingleInstance();
            builder.RegisterType<Repository>().As<IRepository>().InstancePerApiRequest();

            var config = GlobalConfiguration.Configuration;


            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}