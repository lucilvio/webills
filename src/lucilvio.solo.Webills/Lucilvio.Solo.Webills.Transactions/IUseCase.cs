using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    public interface IUseCase<TCommand> where TCommand : ICommand
    {
        Task Execute(TCommand command);
    }
}
