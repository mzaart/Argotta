using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Multilang.Services.ConfigurationServices;

namespace Multilang.Services.MessagingServices.Firebase
{
    public class FirebaseClient
    {
        private readonly IConfigService config;
        private readonly HttpClient client;

        public FirebaseClient(IConfigService config, HttpClient client)
        {
            this.config = config;
            this.client = client;;
        }

        public async Task<HttpResponseMessage> Notify(FcmMessage message)
        {
            var request = new HttpRequestMessage 
            {
                RequestUri = new Uri(config.GetFirbaseUrl() + "/fcm/send"),
                Method = HttpMethod.Post,
                Headers =
                {
                    { HttpRequestHeader.Authorization.ToString(), 
                        "key = " + config.GetFirebaseAuthKey() },
                },
                Content = new StringContent(message.toJson(), 
                    Encoding.UTF8, "application/json")
            };

            return await client.SendAsync(request);
        }
    }
}