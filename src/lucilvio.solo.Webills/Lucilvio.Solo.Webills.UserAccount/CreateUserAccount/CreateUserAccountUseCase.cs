using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    internal class CreateUserAccountUseCase : ICreateUserAccountUseCase
    {
        private readonly ICreateUserAccountDataAccess _dataAccess;

        internal CreateUserAccountUseCase(ICreateUserAccountDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(CreateUserAccountCommand command, Func<UserAccountCreated, Task> onUserAccountCreate)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            var accountWithSameLogin = await _dataAccess.GetUserAccountByLogin(new Domain.Login(command.Login));

            var createUserAccountRule = new CreateUserAccountRule();
            createUserAccountRule.Verify(new Password(command.Password), new Password(command.PasswordConfirmation), accountWithSameLogin);

            var newUser = new User(new Name(command.Name), new Domain.Login(command.Email),
                new Sha1EncryptedPassword(new ComplexPassword(new Password(command.Password))), command.TermsAccepted);

            await _dataAccess.Persist(newUser);

            if(onUserAccountCreate != null)
                await onUserAccountCreate.Invoke(new UserAccountCreated(newUser.Id));
        }

        internal class Error
        {
            public class CommandNotInformed : Exception { }
        }
    }
}