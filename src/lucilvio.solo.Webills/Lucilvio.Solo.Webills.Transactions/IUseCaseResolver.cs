using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal interface IUseCaseResolver
    {
        Task Resolve(ICommand commnad);
    }
}