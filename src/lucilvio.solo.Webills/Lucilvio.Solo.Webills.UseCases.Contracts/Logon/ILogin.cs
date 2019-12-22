using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.Logon
{
    public interface ILogon
    {
        Task Execute(LogonCommand command, Func<LoggedUser, Task> onLogon = null);
    }
}
