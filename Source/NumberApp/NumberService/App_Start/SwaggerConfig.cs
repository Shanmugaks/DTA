using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NumberService
{
    /// <summary>
    /// Description for SwaggerConfig.
    /// </summary>
    public class SwaggerConfig
    {

        /// <summary>
        /// Description for SwaggerConfig:Register().
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("1.0", "Number Web service - Documentation")
                .Description("A Self hosted Number service - Web API Documenttaion")
                .TermsOfService("NO terms & conditions")
                .Contact(cc => cc
                    .Name("Shanmuga")
                    .Url("https://www.linkedin.com/in/shanmuga-ks/")
                    .Email("notifyshan@gmail.com"))
                .License(lc => lc
                    .Name("Free License")
                    .Url("https://en.wikipedia.org/wiki/Software_license"));

                c.PrettyPrint();
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.ResolveConflictingActions(x => x.First());


            }).EnableSwaggerUi(c =>
            {
                c.CustomAsset("index", thisAssembly, "NumberService.CustomSwagger.index.html");
                c.InjectStylesheet(thisAssembly, "NumberService.CustomSwagger.Styles.css");
                c.InjectJavaScript(thisAssembly, "NumberService.CustomSwagger.Scripts.js");
                c.DisableValidator();
                c.DocExpansion(DocExpansion.List);
                c.SupportedSubmitMethods("GET", "HEAD");

            });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{NumberToConvert}",
                defaults: new { NumberToConvert = RouteParameter.Optional }
            );
        }

        /// <summary>
        /// Description for SwaggerConfig:GetXmlCommentsPath().
        /// </summary>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\Numberservice.xml",
                    System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}