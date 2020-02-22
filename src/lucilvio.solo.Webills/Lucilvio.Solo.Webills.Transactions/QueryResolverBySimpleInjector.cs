using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

using SimpleInjector;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal class QueryResolverBySimpleInjector : IQueryResolver
    {
        private readonly Container _container;

        public QueryResolverBySimpleInjector()
        {
            this._container = new Container();

            this._container.Register<TransactionsReadContext>(Lifestyle.Scoped);

            var currentAssembly = typeof(IQuery).Assembly;
            _container.Register(typeof(IQueryHandler<,>), currentAssembly);

            _container.Verify();
        }

        public async Task<TQueryResult> Resolve<TQuery, TQueryResult>(TQuery query)
        {
            var type = typeof(IQueryHandler<,>).MakeGenericType(query.GetType().BaseType);

            dynamic handler = _container.GetInstance(type);
            return await handler.Handle((dynamic)query);
        }
    }
}