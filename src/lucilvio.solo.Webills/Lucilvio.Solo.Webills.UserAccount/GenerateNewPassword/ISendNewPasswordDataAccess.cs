using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal interface ISendNewPasswordDataAccess
    {
        Task<User> GetUserByLogin(Domain.Login login);
        Task Persist();
    }
}