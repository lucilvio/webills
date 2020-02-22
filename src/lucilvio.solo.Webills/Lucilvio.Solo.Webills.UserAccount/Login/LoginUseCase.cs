using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class LoginUseCase : IUseCase<LoginCommand>
    {
        private readonly ILoginDataAccess _dataAccess;

        public LoginUseCase(ILoginDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(LoginCommand command)
        {
            var foundUser = await this._dataAccess.GetUserByLogin(command.Login);

            if (foundUser == null)
                throw new Error.InvalidUserOrPassword();

            if (foundUser.Password != new Sha1EncryptedPassword(new Password(command.Password)))
                throw new Error.InvalidUserOrPassword();
        }

        class Error
        {
            internal class InvalidUserOrPassword : Exception { }
        }
    }
}