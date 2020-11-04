using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateAccount
{
    internal interface ICreateAccountDataAccess
    {
        Task<User> GetUserByLogin(Domain.Login login);
        Task Persist(User user);
    }
}