using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Profile.Domain.User;

namespace Lucilvio.Solo.Webills.Profile.UseCases.RegisterUser
{
    public interface IRegisterUserDataStorage
    {
        Task<User> GetUserByLogin(Login login);
        Task Persist(User user);
    }
}