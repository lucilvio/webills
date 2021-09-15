using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;

namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure.AutofacModule
{
    internal class UserAccountModule : Module
    {
        private readonly IContainer _container;

        public UserAccountModule(Configurations configurations, IEventBus eventBus) : base(configurations, eventBus)
        {
            this._container = this.BuildContainer(configurations, eventBus);
        }

        public override async Task SendMessage(Message message)
        {
            using var scope = this._container.BeginLifetimeScope(builder =>
            {
                builder.RegisterDecorator<Outbox, IEventBus>();
            });

            try
            {                  
                Type handlerType = typeof(IHandler<>).MakeGenericType(message.GetType());
                dynamic handler = scope.Resolve(handlerType);

                await handler.Execute((dynamic)message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IContainer BuildContainer(Configurations configurations, IEventBus eventBus)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(eventBus).SingleInstance();
            builder.RegisterType<OutboxDataAccess>().As<IOutboxDataAccess>();
            builder = this.BuildAutofacModules(builder, configurations, eventBus);

            return builder.Build();
        }

        private ContainerBuilder BuildAutofacModules(ContainerBuilder builder, Configurations configurations, IEventBus eventBus)
        {
            this.GetType().Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(Autofac.Module)))
                .Select(m => (Autofac.Module)Activator.CreateInstance(m, configurations, eventBus))
                .ToList().ToList().ForEach(m => builder.RegisterModule(m));

            return builder;
        }
    }
}