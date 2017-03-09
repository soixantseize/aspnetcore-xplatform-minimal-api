using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RoutingSample.Helpers
{
    public static class HttpExtensions
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        public static Task WriteJson<T>(this HttpResponse response, T obj, int statuscode)
        {
            response.ContentType = "application/json";
            response.StatusCode = statuscode;
            return response.WriteAsync(JsonConvert.SerializeObject(obj));
        }

        public static async Task<T> ReadFromJson<T>(this HttpContext httpContext)
        {
            using (var streamReader = new StreamReader(httpContext.Request.Body))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var obj = Serializer.Deserialize<T>(jsonTextReader);

                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(obj, new ValidationContext(obj), results))
                {
                    return obj;
                }

                //httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteJson(results, 400);

                return default(T);
            }
        }
    }
}
