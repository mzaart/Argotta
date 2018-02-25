using System;
using System.Text;
using System.Linq;
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
                
                    var errorList = context.ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                    foreach (var pair in errorList)
                    {
                        Console.WriteLine(pair.Key + ": " + string.Join(", ", pair.Value));
                    }

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