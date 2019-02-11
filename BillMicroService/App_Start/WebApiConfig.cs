using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BillMicroService
{
    public static class WebApiConfig
    {
        public static void Address(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "product/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
