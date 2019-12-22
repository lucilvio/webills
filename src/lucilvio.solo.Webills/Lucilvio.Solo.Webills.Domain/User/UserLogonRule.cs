using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class UserLogonRule
    {
        private User _user;
        private Login _login;
        private Password _password;

        public UserLogonRule(User user, Login login, Password password)
        {
            this._user = user;
            this._login = login;
            this._password = password;
        }

        public void Verify()
        {
            if (this._user.NotFound())
                throw new InvalidUserOrPassword();

            if (this._user.Login != this._login || this._user.Password != this._password)
                throw new InvalidUserOrPassword();
        }
    }
}
