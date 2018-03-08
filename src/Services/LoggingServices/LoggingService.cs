using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Multilang.Services.ConfigurationServices;

namespace Multilang.Services.LoggingServices
{
    public class LoggingService : ILoggingService
    {
        private IConfigService config;

        public LoggingService(IConfigService config)
        {
            this.config = config;
        }
        
        public async Task LogException(Exception exception)
        {
            var path = config.GetLoggingDir() + $"exceptions-{DateTime.UtcNow.ToString("d")}.txt"
                .Replace("/", "-");
            var text = $"\n\n{DateTime.UtcNow.ToString("f")}:\n"
                + $"Messages: {GetaAllExceptionMessages(exception)}\n"
                + $"{exception.ToString()}\n";
            File.AppendAllText(path, text);
        }

        public async Task LogRequest(HttpRequest request)
        {
            var path = config.GetLoggingDir() + $"requests-{DateTime.UtcNow.ToString("d")}.txt"
                .Replace("/", "-");

            var text = $"\n\n{DateTime.UtcNow.ToString("f")}:\n";
            var requestData = await FormatRequest(request);

            File.AppendAllText(path, text + requestData);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            //var body = request.Body;
            //request.EnableRewind();

            var bodyStr = "";

            // Allows using several time the stream in ASP.Net Core
            request.EnableRewind(); 

            // Arguments: Stream, Encoding, detect encoding, buffer size 
            // AND, the most important: keep stream opened
            using (StreamReader reader 
                    = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }

            // Rewind, so the core is not lost when it looks the body for the request
            request.Body.Position = 0;

            return $"Method: {request.Method}\n"
                + $"Endpoint: {request.Path}\n"
                + $"Headers: {JsonConvert.SerializeObject(request.Headers, Formatting.Indented)}\n"
                + $"Query: {request.QueryString}\n"
                + $"Body: {bodyStr}\n";
        }
        
        private string GetaAllExceptionMessages(Exception exp)
        {
            string message = string.Empty;
            Exception innerException = exp;

            do
            {
                message += (string.IsNullOrEmpty(innerException.Message) ? string.Empty : innerException.Message);
                innerException = innerException.InnerException;
            }
            while (innerException != null);

            return message;
        }
    }
}