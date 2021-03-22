using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    public record GenerateNewPasswordMessage(string Email);

    internal class GenerateNewPasswordMessageHandler : IMessageHandler<GenerateNewPasswordMessage>
    {
        private readonly IGenerateNewPasswordDataAccess _dataAccess;

        public GenerateNewPasswordMessageHandler(IGenerateNewPasswordDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<dynamic> Execute(GenerateNewPasswordMessage input)
        {
            var foundUser = await this._dataAccess.GetUserByEmail(new Email(input.Email));

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newPassword = new AutomaticGeneratedPassword();

            foundUser.ChangePassword(new Sha1EncryptedPassword(newPassword));

            await this._dataAccess.Persist();

            return new GeneratedPassword(foundUser.Name.Value, foundUser.Email.Value, newPassword.Value);
        }

        class Error
        {
            public class UserNotFound : BusinessException { }
        }
    }
}