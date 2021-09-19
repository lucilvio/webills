using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    public record GenerateNewPasswordMessage(string Email) : Message;

    internal class GenerateNewPassword : IUseCase<GenerateNewPasswordMessage>
    {
        private readonly IGenerateNewPasswordDataAccess _dataAccess;
        private readonly IEventPublisher _eventBus;

        public GenerateNewPassword(IGenerateNewPasswordDataAccess dataAccess, IEventPublisher eventBus)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task Execute(GenerateNewPasswordMessage message)
        {
            var foundUser = await this._dataAccess.GetUserByEmail(new Email(message.Email));

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newPassword = new AutomaticGeneratedPassword();

            foundUser.ChangePassword(new Sha1EncryptedPassword(newPassword));

            await this._dataAccess.Persist();

            var generatedPassword = new GeneratedPassword(foundUser.Name.Value, foundUser.Email.Value, newPassword.Value);
            await this._eventBus.Publish(new Event("NewPasswordGenerated", nameof(GenerateNewPassword), generatedPassword));
        }

        class Error
        {
            public class UserNotFound : Module.Error { }
        }
    }
}