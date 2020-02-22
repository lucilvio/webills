using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    public interface IUseCaseResolver
    {
        Task Resolve(ICommand commnad);
    }
}