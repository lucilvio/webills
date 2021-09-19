using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Notification.Infrastructure.DataAccess;
using Lucilvio.Solo.Webills.Notification.Infrastructure.Inbox;
using Newtonsoft.Json;

namespace Lucilvio.Solo.Webills.Notification.Infrastructure.AutofacModule
{
    internal class NotificationModule : Module
    {
        private readonly IContainer _container;

        public NotificationModule(Configurations configurations, IEventPublisher eventBus) : base(configurations, eventBus)
        {
            this._container = this.BuildContainer(configurations, eventBus);
        }

        public override async Task HandleEvent(Event @event)
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

                using var scope = this._container.BeginLifetimeScope(builder =>
                {
                    var inboxType = typeof(Inbox<>).MakeGenericType(messageType);
                    builder.Register(ctx => @event).InstancePerLifetimeScope();
                    builder.RegisterDecorator(inboxType, handlerType);
                });

                dynamic handler = scope.Resolve(handlerType);

                if (handler is null)
                    return;

                var message = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(@event.Payload), messageType);
                await handler.Execute((dynamic)message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IContainer BuildContainer(Configurations configurations, IEventPublisher eventBus)
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