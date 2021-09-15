using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Notification.Infrastructure.DataAccess;
using Lucilvio.Solo.Webills.Notification.Infrastructure.Inbox;
using Lucilvio.Solo.Webills.Notification.NotifyAccountCreation;

namespace Lucilvio.Solo.Webills.Notification.Infrastructure.AutofacModule
{
    internal class NotificationModule : Module
    {
        private readonly IContainer _container;

        public NotificationModule(Configurations configurations, EventsToSubscribe eventsToSubscribe, IEventBus eventBus)
            : base(configurations, eventsToSubscribe, eventBus)
        {
            this._container = this.BuildContainer(configurations, eventBus);
        }

        protected override async Task SubscribeEvents(EventsToSubscribe eventsToSubscribe)
        {
            if (eventsToSubscribe is null || !eventsToSubscribe.HasEvents)
                return;

            foreach (var @event in eventsToSubscribe.Events)
            {
                await this._eventBus.Subscribe(@event, this.HandleEvent);
            }
        }

        private async Task HandleEvent(Event @event)
        {
            try
            {
                var assembly = this.GetType().Assembly;
                var messageType = assembly.GetTypes().FirstOrDefault(t => t.Name.Equals($"{@event.Name}Message", 
                    StringComparison.InvariantCultureIgnoreCase));

                if (messageType is null)
                    return;

                var handlerType = typeof(IHandler<>).MakeGenericType(messageType);

                if (messageType is null || handlerType is null)
                    return;

                using var scope = this._container.BeginLifetimeScope();
                dynamic handler = scope.Resolve(handlerType);

                if (handler is null)
                    return;

                var message = JsonSerializer.Deserialize(@event.Payload, messageType);
                 
                var inbox = new Inbox<AccountCreatedMessage>(@event, handler, scope.Resolve<IInboxDataAccess>());
                await inbox.Execute((dynamic)message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IContainer BuildContainer(Configurations configurations, IEventBus eventBus)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(eventBus).SingleInstance();
            builder.Register(ctx => new NotificationDataContext(configurations.DataConnectionString)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<InboxDataAccess>().As<IInboxDataAccess>().InstancePerLifetimeScope();
            builder = this.BuildAutofacModules(builder, configurations);

            return builder.Build();
        }

        private ContainerBuilder BuildAutofacModules(ContainerBuilder builder, Configurations configurations)
        {
            this.GetType().Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(Autofac.Module)))
                .Select(m => (Autofac.Module)Activator.CreateInstance(m, configurations))
                .ToList().ToList().ForEach(m => builder.RegisterModule(m));

            return builder;
        }
    }
}