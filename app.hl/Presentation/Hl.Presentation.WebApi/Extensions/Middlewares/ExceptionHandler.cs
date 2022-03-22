using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Hl.Core.Application.Commons;
using Hl.Core.Application.Exceptions;
using Hl.Core.Domain.Enums;

namespace Hl.Presentation.Admin.Extensions.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger) =>
            (this.next, this.logger) = (next, logger);


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string titleText = "Internal Server Error.";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;

            switch (exception)
            {
                case DataValidationException e:
                    titleText = "One or more validation errors occurred.";
                    statusCode = (int)e.StatusCode;
                    break;
                //case ValidationException _:
                //    statusCode = (int)HttpStatusCode.BadRequest;
                //    break;
                case Exception _:
                    logger.LogError(exception, exception.Message);
                    exception = new Exception("Internal Server Error");
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(
            JsonConvert.SerializeObject(Result.Failure(
                titleText: titleText,
                statusCode: statusCode,
                traceId: traceId,
                exception: exception
            )));
        }
    }
}
