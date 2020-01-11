using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Security.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.Logon
{
    public interface ILogonDataStorage
    {
        Task<User> GetUserByLogin(string login);
    }
}