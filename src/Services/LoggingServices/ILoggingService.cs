using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Multilang.Services.LoggingServices
{
    public interface ILoggingService
    {
        Task LogRequest(HttpRequest request);
        Task LogException(Exception exception);
    }
}