using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Profile.UseCases.Contracts.RegisterUser
{
    public interface IRegisterUser
    {
        Task Execute(RegisterUserCommand registerUserCommand);
    }
}