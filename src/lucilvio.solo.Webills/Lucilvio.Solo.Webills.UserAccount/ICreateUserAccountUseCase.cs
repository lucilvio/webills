using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public interface ICreateUserAccountUseCase
    {
        Task Execute(CreateUserAccountCommand command, Func<UserAccountCreated, Task> onUserAccountCreate = null);
    }
}