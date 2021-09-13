using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    public record GenerateNewPasswordMessage(string Email) : Message<GeneratedPassword>;

    internal class GenerateNewPassword : IHandler<GenerateNewPasswordMessage>
    {
        private readonly IGenerateNewPasswordDataAccess _dataAccess;

        public GenerateNewPassword(IGenerateNewPasswordDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(GenerateNewPasswordMessage message)
        {
            var foundUser = await this._dataAccess.GetUserByEmail(new Email(message.Email));

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newPassword = new AutomaticGeneratedPassword();

            foundUser.ChangePassword(new Sha1EncryptedPassword(newPassword));

            await this._dataAccess.Persist();

            message.SetResponse(new GeneratedPassword(foundUser.Name.Value, foundUser.Email.Value, newPassword.Value));
        }

        class Error
        {
            public class UserNotFound : Module.Error { }
        }
    }
}