using System;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using Multilang.Models.Responses;
using Multilang.Utils;

namespace Multilang.RequestPipeline.Filters
{
    public class TokenAuth : ActionFilterAttribute
    {
        private IAuthTokenService<JwtBody> tokenService;
        private TransientVars transientVars;

        public TokenAuth(IAuthTokenService<JwtBody> tokenService, TransientVars transientVars)
        {
            this.tokenService = tokenService;
            this.transientVars = transientVars;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string token = context.HttpContext.Request.Headers["Authorization"];
                if (tokenService.IsValid(token))
                {
                    var body = tokenService.GetData(token);
                    var args = context.ActionArguments;
                    args.Select(pair => pair.Key)
                        .Where(k => args[k].GetType() == typeof(JwtBody))
                        .ToList()
                        .ForEach(k =>
                        {
                            args[k] = body;
                            transientVars.TranslationEngine = body.translationEngine;
                        });
                }
                else
                {
                    Reject(context, "Invalid Token");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Reject(context, "An error occurred while verifying token");
            }
        }

        private void Reject(ActionExecutingContext context, String error)
        {
            context.Result = new JsonResult(new BaseResponse(false, error));
        }
    }
}