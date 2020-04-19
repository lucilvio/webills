using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication
{
    public interface IAuthenticationService
    {
        Task SignIn(UserAuthCredentials credentials);
        Task SignOut();

        AuthenticatedUser User();
    }
}