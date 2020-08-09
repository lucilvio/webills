using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal interface IGenerateNewPasswordDataAccess
    {
        Task<User> GetUserByEmail(string email);
        Task Persist();
    }
}