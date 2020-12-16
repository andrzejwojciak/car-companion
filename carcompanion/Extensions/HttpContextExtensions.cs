using System.Linq;
using Microsoft.AspNetCore.Http;

namespace carcompanion.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User == null)
                return null;

            return httpContext.User.Claims.FirstOrDefault( x => x.Type.Equals("sub")).Value;
        }
    }
}