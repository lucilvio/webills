using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public record LoginMessage(string Login, string Password);

    internal class LoginMessageHandler : IMessageHandler<LoginMessage>
    {
        private readonly ILoginDataAccess _dataAccess;

        public LoginMessageHandler(ILoginDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<dynamic> Execute(LoginMessage message)
        {
            var foundUser = await this._dataAccess.GetUserByLogin(new Domain.Login(message.Login));

            if (foundUser == null)
                throw new Error.InvalidUserOrPassword();

            foundUser.Login(new Sha1EncryptedPassword(new Password(message.Password)));

            return new LoggedUser(foundUser);
        }

        class Error
        {
            internal class InvalidUserOrPassword : BusinessException { }
        }
    }
}