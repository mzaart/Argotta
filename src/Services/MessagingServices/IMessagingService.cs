using System.Net.Http;
using System.Threading.Tasks;

namespace Multilang.Services.MessagingServices {
    
    public interface IMessagingService
    {
        Task<HttpResponseMessage> SendMessage(string idFrom, string idTo, string content);
    }
}