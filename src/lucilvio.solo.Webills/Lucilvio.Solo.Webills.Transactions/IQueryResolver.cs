using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal interface IQueryResolver
    {
        Task<TQueryResult> Resolve<TQuery, TQueryResult>(TQuery query);
    }
}