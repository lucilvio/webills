using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class UserAccountModule
    {
        private readonly IBusSubscriber _bus;
        private readonly DependencyResolverWithSimpleInjector _dependencyResolver;

        public UserAccountModule()
        {
            this._dependencyResolver = new DependencyResolverWithSimpleInjector();
            this._bus = this._dependencyResolver.ResolveBusSubscriber();
        }

        public async Task SendMessage<TMessage>(TMessage message)
        {
            if (message == null)
                throw new Error.ComponentInputNotInformed();

            await this._dependencyResolver.ExecuteComponent(message);
        }

        public void SubscribeEvent<TEvent>(Action<TEvent> action)
        {
            this._bus.Subscribe(action);
        }

        internal class Error
        {
            public class ComponentInputNotInformed : Exception { }
        }
    }
}