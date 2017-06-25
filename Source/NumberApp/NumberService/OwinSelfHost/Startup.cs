using Owin;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NumberService
{
    /// <summary>
    /// Description for Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Description for Startup:Configuration().
        /// </summary>
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            // To Support Cross Origin Request support
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            SwaggerConfig.Register(config);
            appBuilder.UseWebApi(config);

        }


    }
}