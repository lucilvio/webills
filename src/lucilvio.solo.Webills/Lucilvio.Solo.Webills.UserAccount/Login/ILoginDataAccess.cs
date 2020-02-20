using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal interface ILoginDataAccess
    {
        Task<User> GetUserByLogin(string login);
    }
}