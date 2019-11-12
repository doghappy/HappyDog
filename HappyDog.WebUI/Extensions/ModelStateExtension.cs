using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace HappyDog.WebUI.Extensions
{
    public static class ModelStateExtension
    {
        public static IList<string> GetErrorMessages(this ModelStateDictionary modelState)
        {
            var list = new List<string>();
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
            }
            return list;
        }
    }
}
