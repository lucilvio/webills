using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    public class TransactionsModule
    {
        IUseCaseResolver useCaseResolver;

        public TransactionsModule()
        {
            this.useCaseResolver = new UseCaseResolverBySimpleInjector();
        }

        public async Task ExecuteCommand(ICommand command)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            await this.useCaseResolver.Resolve(command);
        }

        public class Error
        {
            public class CommandNotInformed : Exception { }
        }
    }
}
