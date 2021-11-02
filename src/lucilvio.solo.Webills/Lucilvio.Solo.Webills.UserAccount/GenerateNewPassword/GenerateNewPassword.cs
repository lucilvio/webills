using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal class GenerateNewPassword : IHandler<GenerateNewPasswordMessage>
    {
        private readonly GenerateNewPasswordDataAccess _dataAccess;
        private readonly IEventPublisher _eventBus;

        public GenerateNewPassword(GenerateNewPasswordDataAccess dataAccess, IEventPublisher eventBus)
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
            public class UserNotFound : Architecture.Error { }
        }
    }

    internal class GeneratedPassword
    {
        internal GeneratedPassword(string userName, string userContact, string password)
        {
            this.UserName = userName;
            this.UserContact = userContact;
            this.Password = password;
        }

        public string UserName { get; }
        public string UserContact { get; }
        public string Password { get; }
    }

    public record GenerateNewPasswordMessage(string Email) : Message;
}