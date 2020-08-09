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
            var user = new User(new Name(input.Name), new Email(input.Email));
            
            var userWithTheSameLogin = await _dataAccess.GetUserByLogin(new Domain.Login(input.Login));

            user.CreateAccount(
                new Domain.Login(input.Email),
                new Sha1EncryptedPassword(new ComplexPassword(new Password(input.Password))),
                new Sha1EncryptedPassword(new Password(input.PasswordConfirmation)),
                input.TermsAccepted,
                userWithTheSameLogin);

            await _dataAccess.Persist(user);

            this._bus.SendEvent(new OnCreatingAccountInput(user));
        }
    }
}