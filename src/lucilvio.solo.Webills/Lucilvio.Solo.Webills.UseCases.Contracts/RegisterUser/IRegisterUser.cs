using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.RegisterUser
{
    public interface IRegisterUser
    {
        Task Execute(RegisterUserCommand registerUserCommand);
    }
}