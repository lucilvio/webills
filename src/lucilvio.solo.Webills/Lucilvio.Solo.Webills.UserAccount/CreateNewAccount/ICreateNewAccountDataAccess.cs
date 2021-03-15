using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    internal interface ICreateNewAccountDataAccess
    {
        Task<User> GetUserByLogin(Domain.Login login);
        Task Persist(User user);
    }
}