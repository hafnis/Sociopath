using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Sociopath.API.Extensions;

namespace Sociopath.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Filters.Add(new ExceptionAttribute());
        }
    }
}
