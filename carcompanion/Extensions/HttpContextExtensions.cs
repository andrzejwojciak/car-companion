using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace carcompanion.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return Guid.Empty;

            try
            {
                var userId = GetValueFromHttpContextClaims(httpContext.User.Claims, "sub");
                return Guid.Parse((userId));
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public static Guid GetAccessTokenJti(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return Guid.Empty;

            try
            {
                var tokenJti = GetValueFromHttpContextClaims(httpContext.User.Claims, "jti");
                return Guid.Parse((tokenJti));
            }
            catch
            {
                return Guid.Empty;
            }
        }

        private static string GetValueFromHttpContextClaims(IEnumerable<Claim> claims, string claimName)
        {
            return claims.FirstOrDefault(claim => claim.Type.Equals(claimName))?.Value;
        }
    }
}