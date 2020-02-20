using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserProfile.Domain;

namespace Lucilvio.Solo.Webills.UserProfile.RegisterUser
{
    interface IRegisterUserDataStorage
    {
        Task<User> GetUserByLogin(Login login);
        Task Persist(User user);
    }
}