using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace ExploreCalifornia.Filters
{
    public class DbUpdateExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (!(context.Exception is DbUpdateException)) return; // just pass this along

            var sqlException = context.Exception?.InnerException?.InnerException as SqlException; // if Exception or Inner Exception 
            //are null, then sql exception will be null, otherwise we'll make it into a sqlexception.

            if (sqlException?.Number == 2627) // 2627 is the number for the violation of a unique constraint (in the database)
                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict);

            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}