using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    public record CreateNewAccountMessage(string Name, string Email, string Password, string PasswordConfirmation, bool TermsAccepted);
    
    internal class CreateNewAccountMessageHandler : IMessageHandler<CreateNewAccountMessage>
    {
        private readonly ICreateNewAccountDataAccess _dataAccess;

        public CreateNewAccountMessageHandler(ICreateNewAccountDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<dynamic> Execute(CreateNewAccountMessage message)
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

            return new CreatedAccount(user);
        }
    }
}