using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IUseCaseResolver
    {
        Task Resolve(ICommand commnad);
    }
}