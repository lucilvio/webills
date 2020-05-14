using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    public class TransactionsModule
    {
        private readonly IBusSubscriber _bus;
        private readonly DependencyResolverWithSimpleInjector _dependencyResolver;

        public TransactionsModule()
        {
            this._dependencyResolver = new DependencyResolverWithSimpleInjector();
            this._bus = this._dependencyResolver.ResolveBusSubscriber();
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

        public void SubscribeEvent<TEvent>(Action<TEvent> action)
        {
            this._bus.Subscribe(action);
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