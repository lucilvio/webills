using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    internal class CreateUserAccountUseCase : IUseCase<CreateUserAccountCommand>
    {
        private readonly ICreateUserAccountDataAccess _dataAccess;

        public   CreateUserAccountUseCase(ICreateUserAccountDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(CreateUserAccountCommand command)
        {
            var accountWithSameLogin = await _dataAccess.GetUserAccountByLogin(new Domain.Login(command.Login));

            var createUserAccountRule = new CreateUserAccountRule();
            createUserAccountRule.Verify(new Password(command.Password), new Password(command.PasswordConfirmation), accountWithSameLogin);

            var newUser = new User(new Name(command.Name), new Domain.Login(command.Email),
                new Sha1EncryptedPassword(new ComplexPassword(new Password(command.Password))), command.TermsAccepted);

            await _dataAccess.Persist(newUser);
        } 
    }
}