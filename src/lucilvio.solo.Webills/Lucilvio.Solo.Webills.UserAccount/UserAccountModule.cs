using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class UserAccountModule : ICreateUserAccountUseCase, ILoginUseCase
    {
        private readonly string _connectionString;

        public UserAccountModule(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task Execute(LoginCommand command, Func<LoggedUser, Task> onLogin = null)
        {
            var dataAccess = new LoginDataAccess(this.Context);
            await new LoginUseCase(dataAccess).Execute(command, onLogin).ConfigureAwait(false);
        }

        public async Task Execute(CreateUserAccountCommand command, Func<UserAccountCreated, Task> onUserAccountCreate = null)
        {
            var dataStorage = new CreateUserAccountDataAccess(this.Context);
            await new CreateUserAccountUseCase(dataStorage).Execute(command, onUserAccountCreate).ConfigureAwait(false);
        }

        private UserAccountContext Context => new UserAccountContext();
    }
}