using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAcessor;

        public AuthenticationService(IHttpContextAccessor httpContextAcessor)
        {
            this._httpContextAcessor = httpContextAcessor;
        }

        public async Task SignIn(UserAuthCredentials credentials)
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

            var context = this._httpContextAcessor.HttpContext;

            if (context == null)
                return;

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
            }).ConfigureAwait(false);
        }

        public async Task SignOut()
        {
            var context = this._httpContextAcessor.HttpContext;

            if (context == null)
                return;

            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public AuthenticatedUser User()
        {
            var context = this._httpContextAcessor.HttpContext;

            if (context == null || !context.User.Identity.IsAuthenticated)
                throw new InvalidOperationException("User is not logged in");

            var name = context.User.Identity.Name;
            var login = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var id = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return new AuthenticatedUser(new Guid(id), login, name);
        }
    }
}