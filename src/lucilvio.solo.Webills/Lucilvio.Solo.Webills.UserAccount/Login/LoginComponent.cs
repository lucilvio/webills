using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class LoginComponent
    {
        private readonly IEventBus _eventBus;
        private readonly ILoginDataAccess _dataAccess;

        public LoginComponent(ILoginDataAccess dataAccess, IEventBus eventBus)
        {
            this._eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(dynamic input)
        {
            var login = new Domain.Login(input.Login);
            
            var foundUser = await this._dataAccess.GetUserByLogin(login);

            if (foundUser == null)
                throw new Error.InvalidUserOrPassword();

            foundUser.Login(new Sha1EncryptedPassword(new Password(input.Password)));

            this._eventBus.Publish(Module.Events.OnLogin, new OnLoginInput(foundUser));
        }

        class Error
        {
            internal class InvalidUserOrPassword : Exception { }
        }
    }
}