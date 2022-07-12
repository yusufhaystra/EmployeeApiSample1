using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace EmployeeApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes(new DefaultInlineConstraintResolver()
            //{
            //    ConstraintMap =
            //    {
            //        ["apiVersion"] = typeof(ApiVersionRouteConstraint)
            //    }
            //}) ;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional}
            );
        }
    }
}
