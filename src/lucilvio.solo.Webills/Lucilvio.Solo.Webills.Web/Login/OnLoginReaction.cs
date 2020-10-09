using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;

namespace Lucilvio.Solo.Webills.Clients.Web.Login
{
    public class OnLoginReaction
    {
        private readonly IAuthenticationService _authenticationService;

        public OnLoginReaction(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        public async Task AuthenticateUser(dynamic authenticatedUser)
        {
            await this._authenticationService.SignIn(new UserAuthCredentials(authenticatedUser.Id,
                authenticatedUser.Login, authenticatedUser.Name));
        }
    }
}
