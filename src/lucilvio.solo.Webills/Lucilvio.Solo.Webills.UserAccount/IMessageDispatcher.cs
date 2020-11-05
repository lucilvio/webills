using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword;
using Lucilvio.Solo.Webills.UserAccount.CreateAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IMessageDispatcher
    {
        Task<LoggedUser> DispatchLoginMessage(ILoginMessage message);
        Task<GeneratedPassword> DispatchForgotYourPasswordMessage(IForgotYourPasswordMessage message);
        Task<CreatedAccount> DispatchCreateNewAccountMessage(ICreateAccountMessage message);
    }
}