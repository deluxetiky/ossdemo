using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OttooDo.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public ExceptionFilter()
        {
        }
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var type = exception.GetType();
            var status = HttpStatusCode.NoContent;
            context.HttpContext.Response.StatusCode = (int)status;
            context.ExceptionHandled = true;
        }
    }
}
