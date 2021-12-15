using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using ExploreCalifornia.Config;
using ExploreCalifornia.ExceptionHandlers;
using ExploreCalifornia.Filters;
using ExploreCalifornia.Loggers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(ExploreCalifornia.Startup))]
namespace ExploreCalifornia
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; set; } = new HttpConfiguration();

        public void Configuration(IAppBuilder app)
        {
            var config = Startup.HttpConfiguration;

            var json = config.Formatters.JsonFormatter.SerializerSettings;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ConfigureWebApi(app, config);
            ConfigureSwashbuckle(config);
            ConfigureJwt(app);
        }

        
        private static void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            //config.Formatters.XmlFormatter.UseXmlSerializer = true; // tells it to use the XML serializer instead of DataContractSerializer
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            config.Filters.Add(new DbUpdateExceptionFilterAttribute());
            config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger()); // interesting that we're passing instances
            // of the newly implemented class rather than just the class name. I.e., why the "new" keyword just there?

            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler()); // same question as above here. 

            config.MessageHandlers.Add(new TokenValidationHandler()); // runs every time someone accesses the API, might put some kind of
            //auth token here, like a string value or a token. 

            config.MapHttpAttributeRoutes(); // look for attributes on action methods
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


        public void ConfigureJwt(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] {GlobalConfig.Audience},
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider(GlobalConfig.Audience, GlobalConfig.Secret)//oh my god when does it end
                }
            }); // why the new keyword here?
        }

        public void ConfigureSwashbuckle(HttpConfiguration config)
        {
            config.EnableSwagger(ConfigObj =>
            {
                
                ConfigObj.SingleApiVersion("v1", "title of api");
                ConfigObj.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}//bin//ExploreCalifornia.xml");
            }).EnableSwaggerUi();
        }
    }
}