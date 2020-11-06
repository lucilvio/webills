using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal class GenerateNewPasswordMessageHandler : IMessageHandler<IGenerateNewPasswordMessage>
    {
        private readonly IEventBus _bus;
        private readonly IGenerateNewPasswordDataAccess _dataAccess;

        public GenerateNewPasswordMessageHandler(IGenerateNewPasswordDataAccess dataAccess, IEventBus bus)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task<dynamic> Execute(IGenerateNewPasswordMessage input)
        {
            var foundUser = await _dataAccess.GetUserByEmail(new Email(input.Email));

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newPassword = new AutomaticGeneratedPassword();

            foundUser.ChangePassword(new Sha1EncryptedPassword(newPassword));

            await _dataAccess.Persist();

            _bus.Publish(Events.OnNewPasswordGenerated.ToString(), new GeneratedPassword(foundUser.Name.Value, foundUser.Email.Value, newPassword.Value));

            return new GeneratedPassword(foundUser.Name.Value, foundUser.Email.Value, newPassword.Value);
        }

        class Error
        {
            public class UserNotFound : BusinessError { }
        }
    }
}