using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Multilang.Services.MessagingServices;
using Multilang.Utils;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using Multilang.Models.Messages;
using System.Threading.Tasks;
using Multilang.RequestPipeline.Filters;

namespace Multilang.Controllers
{
    [ServiceFilter(typeof(TokenAuth))]
    public class ChatController : Controller
    {
        private IMessagingService messagingService;

        public ChatController(IMessagingService messagingService)
        {
            this.messagingService = messagingService;
        }

        [HttpPost("send")]
        public async Task<JsonResult> send([FromBody] SendMessageModel messageModal, JwtBody jwt)
        {
            return null;
        }
    }
}