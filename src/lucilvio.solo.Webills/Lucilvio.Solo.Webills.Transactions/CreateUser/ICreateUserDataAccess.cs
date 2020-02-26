using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.CreateUser
{
    internal interface ICreateUserDataAccess
    {
        Task Persist(User user);
    }
}