using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    public class CreateUserAccountCommandAdapter : CreateUserAccountCommand
    {
        private readonly RegisterRequest _request;

        public CreateUserAccountCommandAdapter(RegisterRequest request)
        {
            this._request = request;
        }

        public override string Login => this._request.Login;

        public override string Password => this._request.Password;

        public override string PasswordConfirmation => this._request.PasswordConfirmation;

        public override string Name => this._request.Name;

        public override bool TermsAccepted => this._request.TermsAccepted;

        public override string Email => this._request.Login;
    }
}