using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserProfile.RegisterUser
{
    public interface IRegisterUser
    {
        Task Register(RegisterUserCommand registerUserCommand);
    }
}