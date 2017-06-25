using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace NumberService
{
    /// <summary>
    /// Description for WebApiConfig.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Description for Register.
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // To Support JSON
            config.Formatters.JsonFormatter.SupportedMediaTypes
            .Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{NumberToConvert}",
                defaults: new { NumberToConvert = RouteParameter.Optional }
            );
        }
    }
}