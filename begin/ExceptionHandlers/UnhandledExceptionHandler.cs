using Newtonsoft.Json;
using System.Web.Http.ExceptionHandling;
using ExploreCalifornia.HttpActionResults;

namespace ExploreCalifornia.ExceptionHandlers
{
    public class UnhandledExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        { 
   //add some compiler directives
#if DEBUG
            var content = JsonConvert.SerializeObject(context.Exception);
#else
            var content = @"{ ""message"": ""Oops, something unexpected happened.""}";
#endif

            context.Result = new ErrorContentResult(content, "application/json", context.Request);


            // gives us the exception, the request, the response, everything

        }
    }
}