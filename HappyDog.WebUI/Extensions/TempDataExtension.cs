using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace HappyDog.WebUI.Extensions
{
    public static class TempDataExtension
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            if (tempData.ContainsKey(key))
            {
                string value = tempData[key].ToString();
                return JsonConvert.DeserializeObject<T>(value);
            }
            return null;
        }
    }
}
