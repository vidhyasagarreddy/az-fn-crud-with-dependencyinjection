using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement
{
    public static class Extensions
    {
        private const string AuthorizationHeader = "Authorization";
        private const string BearerTokenPrefix = "Bearer ";

        public static string GetBearerToken(this HttpRequest request)
        {
            if (request != null &&
                    request.Headers.ContainsKey(AuthorizationHeader) &&
                    request.Headers[AuthorizationHeader].ToString().StartsWith(BearerTokenPrefix))
            {
                return request.Headers[AuthorizationHeader].ToString().Substring(BearerTokenPrefix.Length);
            }

            return null;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }
    }
}
