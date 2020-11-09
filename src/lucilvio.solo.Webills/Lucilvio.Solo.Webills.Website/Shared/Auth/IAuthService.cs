using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Website.Shared.Authorization
{
    public interface IAuthService
    {
        Task SignIn(UserAuthCredentials credentials);
        Task SignOut();
        AuthenticatedUser AuthenticatedUser();
    }
}
