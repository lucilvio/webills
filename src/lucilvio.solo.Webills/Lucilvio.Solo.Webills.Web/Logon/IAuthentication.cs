using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public interface IAuthentication
    {
        Task SignIn(AuthCredentials credentials);
        Task SignOut();

        AuthenticatedUser User();
    }

    public class SecurityService : IAuthentication
    {
        private readonly HttpContext _httpContext;

        public SecurityService(HttpContext httpContext)
        {
            this._httpContext = httpContext;
        }

        public async Task SignIn(AuthCredentials credentials)
        {
            var issuer = "http://localhost:5000";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, credentials.Name, issuer),
                new Claim(ClaimTypes.Email, credentials.Login, issuer),
                new Claim(ClaimTypes.NameIdentifier, credentials.Id.ToString(), issuer)
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

        public async Task SignOut()
        {
            await this._httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public AuthenticatedUser User()
        {
            if (!this._httpContext.User.Identity.IsAuthenticated)
                throw new InvalidOperationException("User is not logged in");

            var name = this._httpContext.User.Identity.Name;
            var login = this._httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var id = this._httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return new AuthenticatedUser(new Guid(id), login, name);
        }
    }
}