using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class LoginUseCase : ILoginUseCase
    {
        private readonly ILoginDataAccess _dataAccess;

        public LoginUseCase(ILoginDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(LoginCommand command, Func<LoggedUser, Task> onLogin)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            var foundUser = await this._dataAccess.GetUserByLogin(command.Login);

            if (foundUser == null)
                throw new Error.InvalidUserOrPassword();

            if (foundUser.Password != new Sha1EncryptedPassword(new Password(command.Password)))
                throw new Error.InvalidUserOrPassword();

            await onLogin?.Invoke(new LoggedUser(foundUser.Id, foundUser.Name.Value, foundUser.Login.Value));
        }

        class Error
        {
            internal class CommandNotInformed : Exception { }
            internal class InvalidUserOrPassword : Exception { }
        }
    }
}