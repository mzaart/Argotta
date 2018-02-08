using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using Multilang.Models.Responses;
using System.Text;
using System.IO;

namespace Multilang.RequestPipeline.Middlewhere
{
    public class ProfilePicAuthMiddlewhere
    {
        private RequestDelegate next;
        private IAuthTokenService<JwtBody> tokenService;
        
        public ProfilePicAuthMiddlewhere(RequestDelegate next, 
            IAuthTokenService<JwtBody> tokenService)
        {
            this.next = next;
            this.tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine(0);
            Console.WriteLine(context.Request.Path);
            // Ignore requests that don't point to static files.
            var path = context.Request.Path;
            if (!path.StartsWithSegments("/images/profile_pics/")
                && !path.StartsWithSegments("/favicon.ico")) {
                Console.WriteLine(1);
                await next.Invoke(context);
                return;
            }

            // if the user is authenticated
            string token = context.Request.Headers["Authorization"];
            if(tokenService.IsValid(token))
            {
                Console.WriteLine(2);
                await next.Invoke(context);
                return;
            }

            Console.WriteLine(3);
            // 401 unauthorized
            context.Response.StatusCode = 401;
            await context.Authentication.ForbidAsync(); // Encoding.UTF8.GetBytes(
                //JsonConvert.SerializeObject(new BaseResponse(false, "Unauthorized")));
        }
    }
}