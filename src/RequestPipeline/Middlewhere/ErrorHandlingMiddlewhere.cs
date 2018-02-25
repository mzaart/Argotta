using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Multilang.Models.Responses;
using Newtonsoft.Json;

namespace Multilang.RequestPipeline.Middlewhere
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception Message: " + ex.Message);
                //Console.WriteLine(ex.StackTrace);

                // write error to file
                var logFile = File.Create("exception.txt");
                var logWriter = new StreamWriter(logFile);
                logWriter.WriteLine(ex.ToString());
                logWriter.Dispose();

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