using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Inbox;
using Lucilvio.Solo.Webills.Notification.Infrastructure.Injection;
using Newtonsoft.Json;

namespace Lucilvio.Solo.Webills.Notification
{
    internal class NotificationModule : Module
    {
        private readonly IContainer _container;

        public NotificationModule(Configurations configurations) : base(configurations)
        {
            this._container = this.BuildContainer(configurations);
        }

        public override async Task HandleEvent(Event @event)
        {
            try
            {
                var messageType = this.GetType().Assembly.GetTypes()
                    .FirstOrDefault(t => t.Name.Equals($"{@event.Name}Message", StringComparison.InvariantCultureIgnoreCase));

                if (messageType is null)
                    return;

                var handlerType = typeof(IHandler<>).MakeGenericType(messageType);
                var inboxType = typeof(Inbox<>).MakeGenericType(messageType);

                if (messageType is null || handlerType is null)
                    return;

                using var scope = this._container.BeginLifetimeScope();

                var message = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(@event.Payload), messageType);

                if(scope.TryResolve(inboxType, out dynamic inbox))
                {
                    await inbox.Execute((dynamic)message, @event);
                    return;
                }

                if(scope.TryResolve(handlerType, out dynamic handler))
                {
                    await handler.Execute((dynamic)message);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IContainer BuildContainer(Configurations configurations)
        {
            var builder = new ContainerBuilder();
            builder = this.BuildAutofacModules(builder, configurations);

            return builder.Build();
        }

        private ContainerBuilder BuildAutofacModules(ContainerBuilder builder, Configurations configurations)
        {
            this.GetType().Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(AutofacFactory)) && !t.IsInterface && !t.IsAbstract)
                .Select(m => (Autofac.Module)Activator.CreateInstance(m, configurations))
                .ToList().ToList().ForEach(m => builder.RegisterModule(m));

            return builder;
        }
    }
}