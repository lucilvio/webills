using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    internal class CreateUserAccountComponent
    {
        private readonly ICreateUserAccountDataAccess _dataAccess;
        private readonly IBusSender _bus;

        public CreateUserAccountComponent(ICreateUserAccountDataAccess dataAccess, IBusSender bus)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(CreateUserAccountInput input)
        {
            var accountWithSameLogin = await _dataAccess.GetUserAccountByLogin(new Domain.Login(input.Login));

            var createUserAccountRule = new CreateUserAccountRule();
            createUserAccountRule.Verify(new Password(input.Password), new Password(input.PasswordConfirmation), accountWithSameLogin);

            var newUser = new User(new Name(input.Name), new Domain.Login(input.Email),
                new Sha1EncryptedPassword(new ComplexPassword(new Password(input.Password))), input.TermsAccepted);

            await _dataAccess.Persist(newUser);

            this._bus.SendEvent(new OnCreatingAccountInput(newUser));
        }
    }
}