using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    internal class CreateNewAccount : IMessageHandler<CreateNewAccountMessage>
    {
        private readonly CreateNewAccountDataAccess _dataAccess;
        private readonly IEventPublisher _eventPublisher;

        public CreateNewAccount(CreateNewAccountDataAccess dataAccess, IEventPublisher eventPublisher)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
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

            await this._eventPublisher.Publish(new AccountCreated(new CreatedAccount(user)));
        }
    }

    internal class CreatedAccount
    {
        public CreatedAccount(User user)
        {
            if (user is null)
                return;

            this.UserName = user.Name.Value;
        }

        public string UserName { get; }
    }

    [Outbox]
    public record CreateNewAccountMessage(string Name, string Email, string Password,
        string PasswordConfirmation, bool TermsAccepted) : Message;

    public class AccountCreated : Event
    {
        public AccountCreated(object payload) : base(nameof(AccountCreated), nameof(CreateNewAccount), payload) { }
    }
}