using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    internal interface ICreateUserAccountDataAccess
    {
        Task<User> GetUserAccountByLogin(Domain.Login login);
        Task Persist(User user);
    }
}