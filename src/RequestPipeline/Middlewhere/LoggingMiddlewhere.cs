using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Multilang.Services.LoggingServices;

namespace Multilang.RequestPipeline.Middlewhere
{
    public class LoggingMiddleWhere
    {
        private readonly RequestDelegate next;
        private readonly ILoggingService logger;

        public LoggingMiddleWhere(RequestDelegate next, ILoggingService logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await logger.LogRequest(context.Request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error writing request logs.");
            }
            await next(context);
        }
    }
}