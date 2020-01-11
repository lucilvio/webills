using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Security.Domain.User;
using Lucilvio.Solo.Webills.Security.UseCases.Contracts.Logon;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;

namespace Lucilvio.Solo.Webills.UseCases.Logon
{
    public class Logon : ILogon
    {
        private readonly ILogonDataStorage _dataStorage;

        public Logon(ILogonDataStorage dataStorage)
        {
            this._dataStorage = dataStorage ?? throw new DataStorageNotInformed();
        }

        public async Task Execute(LogonCommand command, Func<LoggedUser, Task> onLogon = null)
        {
            if (command == null)
                throw new CommandNotInformed();

            var user = await this._dataStorage.GetUserByLogin(command.Login).ConfigureAwait(false);

            var userLogonRule = new UserLogonRule(user, command.Login, command.Password);
            userLogonRule.Verify();

            if (onLogon != null)
                await onLogon.Invoke(new LoggedUser(user.Id, user.Name, user.Login));
        }
    }
}