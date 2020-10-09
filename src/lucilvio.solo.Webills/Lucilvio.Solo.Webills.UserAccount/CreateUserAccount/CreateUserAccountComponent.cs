using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    internal class CreateUserAccountComponent
    {
        private readonly IEventBus _eventBus;
        private readonly ICreateUserAccountDataAccess _dataAccess;

        public CreateUserAccountComponent(ICreateUserAccountDataAccess dataAccess, IEventBus eventBus)
        {
            this._eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(dynamic input)
        {
            var user = new User(new Name(input.Name), new Email(input.Email));

            var userWithTheSameLogin = await this._dataAccess.GetUserByLogin(new Domain.Login(input.Login));

            user.CreateAccount(
                new Domain.Login(input.Email),
                new Sha1EncryptedPassword(new ComplexPassword(new Password(input.Password))),
                new Sha1EncryptedPassword(new Password(input.PasswordConfirmation)),
                input.TermsAccepted,
                userWithTheSameLogin);

            await this._dataAccess.Persist(user);

            this._eventBus.Publish(Module.Events.OnAccountCreated, new OnCreatingAccountInput(user));
        }
    }
}