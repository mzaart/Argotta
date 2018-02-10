using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Multilang.Services.AuthTokenServices;
using Multilang.Models.Jwt;
using Multilang.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace Multilang.RequestPipeline.Filters
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == "POST")
            {
                if (!context.ModelState.IsValid)
                {
                    Console.WriteLine(11);
                    Reject(context, "Invalid Model");
                }
            }
        }

        private void Reject(ActionExecutingContext context, String error)
        {
            context.Result = new JsonResult(new BaseResponse(false,  error));
        }
    }
}