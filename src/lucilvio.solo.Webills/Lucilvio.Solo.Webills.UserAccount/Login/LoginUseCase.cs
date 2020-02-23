using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Bus;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class LoginUseCase : IUseCase<LoginCommand>
    {
        private readonly IBus _bus;
        private readonly ILoginDataAccess _dataAccess;

        public LoginUseCase(ILoginDataAccess dataAccess, IBus bus)
        {
            this._bus = bus;
            this._dataAccess = dataAccess;
        }

        public async Task Execute(LoginCommand command)
        {
            var foundUser = await this._dataAccess.GetUserByLogin(command.Login);

            if (foundUser == null)
                throw new Error.InvalidUserOrPassword();

            if (foundUser.Password != new Sha1EncryptedPassword(new Password(command.Password)))
                throw new Error.InvalidUserOrPassword();

            await this._bus.SendEvent(new { userId = foundUser.Id, userName = foundUser.Name.Value, userLogin = foundUser.Login.Value });
        }

        class Error
        {
            internal class InvalidUserOrPassword : Exception { }
        }
    }
}