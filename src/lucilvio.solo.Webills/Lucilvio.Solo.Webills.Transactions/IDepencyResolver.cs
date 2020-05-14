using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal interface IDependencyResolver
    {
        Task<dynamic> ExecuteComponent<TComponentInput>(TComponentInput input);
    }
}
