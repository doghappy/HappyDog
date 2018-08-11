using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Net;

namespace HappyDog.WebUI.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                base.OnActionExecuting(context);
            }
            else
            {
                List<string> msg = new List<string>();
                foreach (var value in context.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        msg.Add(error.ErrorMessage);
                    }
                }
                context.Result = new JsonResult(new HttpDataResult<List<string>>(true)
                {
                    Status = HttpStatusCode.BadRequest,
                    Notify = NotifyResult.Warning,
                    Data = msg
                });
            }
        }
    }
}
