using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public interface ISecurityService
    {
        Task Authenticate(Credentials credentials);
    }

    public class SecurityService : ISecurityService
    {
        private readonly HttpContext _httpContext;

        public SecurityService(HttpContext httpContext)
        {
            this._httpContext = httpContext;
        }

        public async Task Authenticate(Credentials credentials)
        {
            var issuer = "http://localhost:5000";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, credentials.Name, issuer),
                new Claim(ClaimTypes.NameIdentifier, credentials.Login, issuer)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            var claimsPrincipal = new ClaimsPrincipal(userIdentity);

            await this._httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
            }).ConfigureAwait(false);
        }
    }
}