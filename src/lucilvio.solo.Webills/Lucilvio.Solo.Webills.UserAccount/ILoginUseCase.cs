using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public interface ILoginUseCase
    {
        Task Execute(LoginCommand command, Func<LoggedUser, Task> onLogin = null);
    }
}
