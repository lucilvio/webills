using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Domain.Security.User;

namespace Lucilvio.Solo.Webills.UseCases.Logon
{
    public interface ILogonDataStorage
    {
        Task<User> GetUserByLogin(string login);
    }
}