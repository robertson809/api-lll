using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using ExploreCalifornia.Config;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(ExploreCalifornia.Startup))]
namespace ExploreCalifornia
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; set; } = new HttpConfiguration();

        public void Configuration(IAppBuilder app)
        {
            var config = Startup.HttpConfiguration;
            ConfigureWebApi(app, config);
        }

        
        private static void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);


            // this tells the API calls where to look for the methods. 
            // here they look in tour and get, so when we call http://localhost:52201/api/tour
            // we look for a class that has "tour" in its name within the Controllers files. 
            // Controllers is a keyword file. 
            // if in the controllers folder it finds a class that starts with tour and a method that starts
            // with get, it will execute it.  
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                // don't have to match the id
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}