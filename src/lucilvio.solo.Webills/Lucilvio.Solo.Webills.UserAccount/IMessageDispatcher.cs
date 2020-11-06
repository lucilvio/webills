using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.UserAccount.CreateAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IMessageDispatcher
    {
        Task<LoggedUser> DispatchLoginMessage(ILoginMessage message);
        Task<GeneratedPassword> DispatchGenerateNewPasswordMessage(IGenerateNewPasswordMessage message);
        Task<CreatedAccount> DispatchCreateNewAccountMessage(ICreateNewAccountMessage message);
    }
}