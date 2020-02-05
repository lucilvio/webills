using Lucilvio.Solo.Webills.Security.Domain.User.BusinessErrors;
using Lucilvio.Solo.Webills.Shared.Domain;

namespace Lucilvio.Solo.Webills.Security.Domain.User
{
    public class UserLogonRule
    {
        private User _user;
        private string _login;
        private string _password;

        public UserLogonRule(User user, string login, string password)
        {
            this._user = user;
            this._login = login;
            this._password = password;
        }

        public void Verify()
        {
            if (this._user.NotDefined())
                throw new InvalidUserOrPassword();

            if (this._user.Login != this._login || this._user.Password != this._password)
                throw new InvalidUserOrPassword();
        }
    }
}
