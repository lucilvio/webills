using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    public class CreateUserAccountInputAdapter : ICreateUserAccountInput
    {
        private readonly SignUpRequest _request;

        public CreateUserAccountInputAdapter(SignUpRequest request)
        {
            this._request = request;
        }

        public string Login => this._request.Login;
        public string Password => this._request.Password;
        public string PasswordConfirmation => this._request.PasswordConfirmation;
        public string Name => this._request.Name;
        public bool TermsAccepted => this._request.TermsAccepted;
        public string Email => this._request.Login;
    }
}