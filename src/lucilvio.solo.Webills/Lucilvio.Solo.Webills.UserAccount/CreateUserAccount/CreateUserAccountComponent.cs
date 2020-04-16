using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    internal class CreateUserAccountComponent
    {
        private readonly ICreateUserAccountDataAccess _dataAccess;

        public CreateUserAccountComponent(ICreateUserAccountDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(CreateUserAccountInput input, Func<UserAccountCreated, Task> onCreate)
        {
            var accountWithSameLogin = await _dataAccess.GetUserAccountByLogin(new Domain.Login(input.Login));

            var createUserAccountRule = new CreateUserAccountRule();
            createUserAccountRule.Verify(new Password(input.Password), new Password(input.PasswordConfirmation), accountWithSameLogin);

            var newUser = new User(new Name(input.Name), new Domain.Login(input.Email),
                new Sha1EncryptedPassword(new ComplexPassword(new Password(input.Password))), input.TermsAccepted);

            await _dataAccess.Persist(newUser);

            if (onCreate != null)
                onCreate(new UserAccountCreated(newUser));
        }
    }
}