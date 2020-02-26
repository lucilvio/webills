using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.Clients.Web.Login
{
    internal class LoginInputAdapter : LoginInput
    {
        private readonly LoginRequest _request;

        public LoginInputAdapter(LoginRequest request)
        {
            if (request == null)
                return;

            _request = request;
        }

        public override string Login => _request.Login;

        public override string Password => _request.Password;
    }
}