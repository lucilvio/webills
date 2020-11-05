using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Transactions.CreateUser;

namespace Lucilvio.Solo.Webills.Transactions
{
    public class TransactionsModule
    {
        private readonly IEventBus _eventBus;
        private readonly DependencyResolverWithSimpleInjector _dependencyResolver;

        public TransactionsModule(IEventBus eventBus)
        {
            this._eventBus = eventBus;
            this._dependencyResolver = new DependencyResolverWithSimpleInjector();

            this._eventBus.Subscibe("UserAccountCreated", async userAccount =>
            {
                await SendMessage<CreateUserInput>(new CreateUserInput(userAccount.Id));
            });
        }

        public async Task SendMessage<TMessage>(TMessage message)
        {
            this.VerifyInput(message);

            await this._dependencyResolver.ExecuteComponent(message);
        }

        public async Task<TOutputMessage> SendMessage<TMessage, TOutputMessage>(TMessage message)
        {
            this.VerifyInput(message);

            return await this._dependencyResolver.ExecuteComponent<TMessage, TOutputMessage>(message);
        }

        private void VerifyInput(object input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();
        }

        internal class Error
        {
            public class ComponentInputNotInformed : Exception { }
            public class QueryNotInformed : Exception { }
        }
    }
}