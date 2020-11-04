using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword
{
    internal class ForgotYourPasswordMessageHandler : IMessageHandler<IForgotYourPasswordMessage>
    {
        private readonly IEventBus _bus;
        private readonly IForgotYourPasswordDataAccess _dataAccess;

        public ForgotYourPasswordMessageHandler(IForgotYourPasswordDataAccess dataAccess, IEventBus bus)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task<dynamic> Execute(IForgotYourPasswordMessage input)
        {
            var foundUser = await this._dataAccess.GetUserByEmail(new Email(input.Email));

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newPassword = new AutomaticGeneratedPassword();

            foundUser.ChangePassword(new Sha1EncryptedPassword(newPassword));

            await this._dataAccess.Persist();

            this._bus.Publish(Module.Events.OnNewPasswordGenerated, new GeneratedPassword(foundUser.Name.Value, foundUser.Email.Value, newPassword.Value));

            return new object();
        }

        class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}