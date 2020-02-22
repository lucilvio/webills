using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IUseCase<TCommand> where TCommand : ICommand
    {
        Task Execute(TCommand command);
    }
}