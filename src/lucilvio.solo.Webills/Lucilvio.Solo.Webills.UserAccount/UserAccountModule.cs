using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure.Injection;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal class UserAccountModule : Module
    {
        private readonly IContainer _container;

        public UserAccountModule(Configurations configurations, IEventPublisher eventBus) : base(configurations, eventBus)
        {
            this._container = this.BuildContainer(configurations, eventBus);
        }

        public override async Task SendMessage(Message message)
        {
            using var scope = this._container.BeginLifetimeScope(builder =>
            {
                builder.RegisterDecorator<Outbox, IEventPublisher>();
            });

            try
            {
                Type handlerType = typeof(IUseCase<>).MakeGenericType(message.GetType());
                dynamic handler = scope.Resolve(handlerType);

                await handler.Execute((dynamic)message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IContainer BuildContainer(Configurations configurations, IEventPublisher eventBus)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(eventBus).SingleInstance();
            builder.RegisterType<OutboxDataAccess>().As<IOutboxDataAccess>();
            builder = this.BuildAutofacFactories(builder, configurations, eventBus);

            return builder.Build();
        }

        private ContainerBuilder BuildAutofacFactories(ContainerBuilder builder, Configurations configurations, IEventPublisher eventBus)
        {
            this.GetType().Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(AutofacFactory)) && !t.IsInterface && !t.IsAbstract)
                .Select(m => (Autofac.Module)Activator.CreateInstance(m, configurations, eventBus))
                .ToList().ToList().ForEach(m => builder.RegisterModule(m));

            return builder;
        }
    }
}