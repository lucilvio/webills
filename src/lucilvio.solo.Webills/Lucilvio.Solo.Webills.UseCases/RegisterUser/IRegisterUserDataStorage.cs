using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.Profile.User;

namespace Lucilvio.Solo.Webills.UseCases.RegisterUser
{
    public interface IRegisterUserDataStorage
    {
        Task<User> GetUserByLogin(Login login);
        Task Persist(User user);
    }
}