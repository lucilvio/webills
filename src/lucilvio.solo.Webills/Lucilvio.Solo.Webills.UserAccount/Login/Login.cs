using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class Login : IMessageHandler<LoginMessage>
    {
        private readonly LoginDataAccess _dataAccess;

        public Login(LoginDataAccess dataAccess)
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

    public record LoginMessage(string Login, string Password) : Message<LoggedUser>;

    public class LoggedUser
    {
        internal LoggedUser(User user)
        {
            if (user is null)
                return;

            this.Id = user.Id;
            this.Name = user.Name.Value;
            this.Email = user.Email.Value;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}