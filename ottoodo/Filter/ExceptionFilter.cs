using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OttooDo.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        public ExceptionFilter()
        {
            _logger = Log.Logger.ForContext<ExceptionFilter>();
        }
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.Error(context.Exception, "Error");
            var type = exception.GetType();
            var status = HttpStatusCode.NoContent;
            context.HttpContext.Response.StatusCode = (int)status;
            context.ExceptionHandled = true;
        }
    }
}
