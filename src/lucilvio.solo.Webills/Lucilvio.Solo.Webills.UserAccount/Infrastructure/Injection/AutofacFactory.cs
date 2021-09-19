using Lucilvio.Solo.Webills.EventBus;

namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure.Injection
{
    internal abstract class AutofacFactory : Autofac.Module
    {
        protected readonly IEventPublisher _eventBus;
        protected readonly Module.Configurations _configurations;

        public AutofacFactory(Module.Configurations configurations, IEventPublisher eventBus)
        {
            this._eventBus = eventBus;
            this._configurations = configurations;
        }
    }
}
