using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public record LoginMessage(string Login, string Password) : Message<LoggedUser>;

    internal class Login : IHandler<LoginMessage>
    {
        private readonly ILoginDataAccess _dataAccess;

        public Login(ILoginDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(LoginMessage message)
        {
            var foundUser = await this._dataAccess.GetUserByLogin(new Domain.Login(message.Login));

            if (foundUser == null)
                throw new Error.InvalidUserOrPassword();

            foundUser.Login(new Sha1EncryptedPassword(new Password(message.Password)));

            message.SetResponse(new LoggedUser(foundUser));
        }

        class Error
        {
            internal class InvalidUserOrPassword : Architecture.Error { }
        }
    }
}