using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal class GenerateNewPasswordComponent
    {
        private readonly IGenerateNewPasswordDataAccess _dataAccess;
        private readonly IBusSender _bus;

        public GenerateNewPasswordComponent(IGenerateNewPasswordDataAccess dataAccess, IBusSender bus)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(GenerateNewPasswordInput input)
        {
            var foundUser = await this._dataAccess.GetUserByLogin(new Domain.Login(input.Email));

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newPassword = new AutomaticGeneratedPassword();

            foundUser.ChangePassword(new Sha1EncryptedPassword(newPassword));

            await this._dataAccess.Persist();

            this._bus.SendEvent(new OnGeneratingPasswordInput(foundUser.Name.Value, foundUser.Login.Value, newPassword.Value));
        }

        class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}