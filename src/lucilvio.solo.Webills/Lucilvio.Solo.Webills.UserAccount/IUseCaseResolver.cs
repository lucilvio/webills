using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public interface IUseCaseResolver
    {
        Task Resolve(ICommand commnad);
    }
}