using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.Clients.Web.Login
{
    public class LoginRequest : ILoginMessage
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}