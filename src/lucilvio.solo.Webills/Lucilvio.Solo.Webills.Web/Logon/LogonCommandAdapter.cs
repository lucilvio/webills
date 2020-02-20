using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    internal class LoginCommandAdapter : LoginCommand
    {
        private readonly LogonRequest _request;

        public LoginCommandAdapter(LogonRequest request)
        {
            if (request == null)
                return;

            this._request = request;
        }

        public override string Login => this._request.Login;

        public override string Password => this._request.Password;
    }
}