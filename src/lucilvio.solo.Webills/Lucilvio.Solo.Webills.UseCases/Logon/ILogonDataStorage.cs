using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.Logon
{
    public interface ILogonDataStorage
    {
        Task<User> GetUserByLogin(Login login);
    }
}