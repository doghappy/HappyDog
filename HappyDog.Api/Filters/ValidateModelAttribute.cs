﻿using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HappyDog.Api.Filters
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
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new HttpBaseResult
                {
                    NoticeMode = NoticeMode.Warning,
                    Message = string.Join('\t', msg)
                });
            }
        }
    }
}
