using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Multilang.Models.Responses;
using Multilang.Services.LoggingServices;
using Newtonsoft.Json;

namespace Multilang.RequestPipeline.Middlewhere
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private ILoggingService logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggingService logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await logger.LogException(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new BaseResponse(false, code.ToString()));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }
    }
}