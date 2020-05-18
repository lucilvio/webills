using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class LoginComponent
    {
        private readonly ILoginDataAccess _dataAccess;
        private readonly IBusSender _bus;

        public LoginComponent(ILoginDataAccess dataAccess, IBusSender bus)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(LoginInput input)
        {
            var login = new Domain.Login(input.Login);
            
            var foundUser = await this._dataAccess.GetUserByLogin(login);

            if (foundUser == null)
                throw new Error.InvalidUserOrPassword();

            if (foundUser.Password != new Sha1EncryptedPassword(new Password(input.Password)))
                throw new Error.InvalidUserOrPassword();

            this._bus.SendEvent(new OnLoginInput(foundUser));
        }

        class Error
        {
            internal class InvalidUserOrPassword : Exception { }
        }
    }
}