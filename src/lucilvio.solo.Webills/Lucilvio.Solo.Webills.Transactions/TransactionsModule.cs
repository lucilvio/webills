using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    public class TransactionsModule
    {
        private IQueryResolver _queryResolver;
        private IUseCaseResolver _useCaseResolver;

        public TransactionsModule()
        {
            this._useCaseResolver = new UseCaseResolverBySimpleInjector();
        }

        public async Task ExecuteCommand(ICommand command)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            await this._useCaseResolver.Resolve(command);
        }

        public async Task<TQueryResult> ExecuteQuery<TQueryResult>(IQuery query)
        {
            if (query == null)
                throw new Error.QueryNotInformed();

            return await this._queryResolver.Resolve<IQuery, TQueryResult>(query);
        }

        public class Error
        {
            public class CommandNotInformed : Exception { }
            public class QueryNotInformed : Exception { }
        }
    }
}