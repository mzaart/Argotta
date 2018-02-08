using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using Multilang.Models.Responses;

namespace Multilang.Filters
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
                    //context.ActionArguments.Add("email", body.email);
                }
                else
                {
                    Reject(context);
                }
            }
            catch(Exception e)
            {
                Reject(context);
            }
        }

        private void Reject(ActionExecutingContext context)
        {
            context.Result = new JsonResult(new BaseResponse(false, "Invalid token."));
        }
    }
}