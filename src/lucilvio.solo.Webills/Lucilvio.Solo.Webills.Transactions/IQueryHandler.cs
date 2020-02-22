using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal interface IQueryHandler<TQuery, TQueryResult> where TQuery : IQuery
    {
        Task<TQueryResult> Handle(TQuery query);
    }
}