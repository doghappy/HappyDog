using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace HappyDog.Api.Infrastructure.Extensions
{
    public static class ControllerExtension
    {
        public static List<string> GetModelStateErrorMsg(this ModelStateDictionary modelState)
        {
            List<string> msg = new List<string>();
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    msg.Add(error.ErrorMessage);
                }
            }
            return msg;
        }
    }
}
