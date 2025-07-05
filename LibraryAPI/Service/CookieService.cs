using Azure;
using LibraryAPI.Common.Constants;
using LibraryAPI.Interface.Service;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace LibraryAPI.Service
{
    public class CookieService : ICookieService
    {
        public CookieService()
        {
            
        }

        public void CreateSessionCookie(string sessionToken, HttpRequest request, HttpResponse response)
        {
            var requestHost = request.Host.Host?.ToLowerInvariant();
            var isLocalhost = string.IsNullOrEmpty(requestHost) || requestHost.Contains("localhost");
            string? cookieDomain = null;

            if (!isLocalhost)
            {
                cookieDomain = requestHost;
            }

            response.Cookies.Append(CookieNames.SessionToken, sessionToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = !isLocalhost,
                SameSite = isLocalhost ? SameSiteMode.Lax : SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddMinutes(5),
                Domain = cookieDomain,
                Path = "/"
            });
        }

        public void CreateRefreshCookie(string refreshToken, HttpRequest request, HttpResponse response)
        {
            var requestHost = request.Host.Host?.ToLowerInvariant();
            var isLocalhost = string.IsNullOrEmpty(requestHost) || requestHost.Contains("localhost");
            string? cookieDomain = null;

            if (!isLocalhost)
            {
                cookieDomain = requestHost;
            }

            response.Cookies.Append(CookieNames.RefreshToken, refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = !isLocalhost,
                SameSite = isLocalhost ? SameSiteMode.Lax : SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Domain = cookieDomain,
                Path = "/"
            });
        }
    }
}
