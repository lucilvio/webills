using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal class GenerateNewPasswordComponent
    {
        private readonly ISendNewPasswordDataAccess _dataAccess;

        public GenerateNewPasswordComponent(ISendNewPasswordDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<GeneratedPassword> Execute(SendNewPasswordInput input)
        {
            var foundUser = await _dataAccess.GetUserByLogin(new Domain.Login(input.Email));

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newPassword = new AutomaticGeneratedPassword();

            foundUser.ChangePassword(new Sha1EncryptedPassword(newPassword));

            await _dataAccess.Persist();

            return new GeneratedPassword(foundUser.Name.Value, foundUser.Login.Value, newPassword.Value);
        }

        class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}