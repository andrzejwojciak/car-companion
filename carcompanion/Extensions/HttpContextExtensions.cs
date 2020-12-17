using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace carcompanion.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User == null)
                return Guid.Empty;

            try
            {
                var userId = Guid.Parse(httpContext.User.Claims.FirstOrDefault( x => x.Type.Equals("sub")).Value); 
                return userId;
            }
            catch
            {
                return Guid.Empty;
            }

        }
    }
}