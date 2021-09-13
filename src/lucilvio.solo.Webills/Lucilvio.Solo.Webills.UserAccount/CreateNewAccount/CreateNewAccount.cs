using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    public record CreateNewAccountMessage(string Name, string Email, string Password,
        string PasswordConfirmation, bool TermsAccepted) : Message;

    internal class CreateNewAccount : IHandler<CreateNewAccountMessage>
    {
        private readonly ICreateNewAccountDataAccess _dataAccess;

        public CreateNewAccount(ICreateNewAccountDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(CreateNewAccountMessage message)
        {
            var user = new User(new Name(message.Name), new Email(message.Email));

            var userWithTheSameLogin = await this._dataAccess.GetUserByLogin(new Domain.Login(message.Email));

            user.CreateAccount(
                new Domain.Login(message.Email),
                new Sha1EncryptedPassword(new ComplexPassword(new Password(message.Password))),
                new Sha1EncryptedPassword(new Password(message.PasswordConfirmation)),
                message.TermsAccepted,
                userWithTheSameLogin);

            await this._dataAccess.Persist(user);
        }
    }    
}