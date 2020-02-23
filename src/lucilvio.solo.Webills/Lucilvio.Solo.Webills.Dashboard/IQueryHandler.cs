using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Dashboard
{
    internal interface IQueryHandler<TQuery, TQueryResult>
    {
        Task<TQueryResult> Handle(TQuery query);
    }
}