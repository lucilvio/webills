using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Dashboard
{
    internal interface IQueryResolver
    {
        Task<TQueryResult> Resolve<TQueryResult>(IQuery query);
    }
}