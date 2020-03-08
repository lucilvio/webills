using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class LoginComponent
    {

        private readonly ILoginDataAccess _dataAccess;

        public LoginComponent(ILoginDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(LoginInput input, Func<LoggedUser, Task> onLogin)
        {
            var foundUser = await this._dataAccess.GetUserByLogin(input.Login);

            if (foundUser == null)
                throw new Error.InvalidUserOrPassword();

            if (foundUser.Password != new Sha1EncryptedPassword(new Password(input.Password)))
                throw new Error.InvalidUserOrPassword();

            if (onLogin != null)
                await onLogin.Invoke(new LoggedUser(foundUser));
        }

        class Error
        {
            internal class InvalidUserOrPassword : Exception { }
        }
    }
}