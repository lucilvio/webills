using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Dashboard
{
    public class DashboardModule
    {
        private readonly IQueryResolver _queryResolver;

        public DashboardModule()
        {
            this._queryResolver = new QueryResolverBySimpleInjector();
        }

        public async Task<TQueryResult> ExecuteQuery<TQueryResult>(IQuery query)
        {
            return await this._queryResolver.Resolve<TQueryResult>(query);
        }
    }
}
