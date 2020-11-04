using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword
{
    internal interface IForgotYourPasswordDataAccess
    {
        Task<User> GetUserByEmail(Email email);
        Task Persist();
    }
}