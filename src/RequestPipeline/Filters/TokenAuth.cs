using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using Multilang.Models.Responses;

namespace Multilang.RequestPipeline.Filters
{
    public class TokenAuth : ActionFilterAttribute
    {
        private IAuthTokenService<JwtBody> tokenService;

        public TokenAuth(IAuthTokenService<JwtBody> tokenService)
        {
            this.tokenService = tokenService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string token = context.HttpContext.Request.Headers["Authorization"];
                if(tokenService.IsValid(token))
                {
                    var body = tokenService.GetData(token);
                    context.ActionArguments["jwtBody"] = body;
                }
                else
                {
                    Console.WriteLine(1);
                    Reject(context, "Invalid Token");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Reject(context, "An error occurred while verifying token");
            }
        }

        private void Reject(ActionExecutingContext context, String error)
        {
            context.Result = new JsonResult(new BaseResponse(false,  error));
        }
    }
}