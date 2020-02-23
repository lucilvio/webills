using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.Dashboard
{
    internal class QueryResolverBySimpleInjector : IQueryResolver
    {
        private readonly Container _container;

        public QueryResolverBySimpleInjector()
        {
            this._container = new Container();
            this._container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            this._container.Register<DashBoardReadContext>(Lifestyle.Scoped);

            var currentAssembly = typeof(IQuery).Assembly;
            _container.Register(typeof(IQueryHandler<,>), currentAssembly);

            _container.Verify();
        }

        public async Task<TQueryResult> Resolve<TQueryResult>(IQuery query)
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var type = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TQueryResult));

                dynamic handler = _container.GetInstance(type);
                return await handler.Handle((dynamic)query);
            }
        }
    }
}