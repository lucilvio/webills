using Autofac;
using Lucilvio.Solo.Architecture.Handler.Inbox;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Modules.AutofacModule
{
    public class AutofacModuleResolver<TModule> : IModuleResolver<TModule> where TModule : IModule
    {
        private readonly IContainer _container;

        public AutofacModuleResolver(object configurations)
        {
            if (configurations is null)
                throw new ArgumentNullException(nameof(configurations));

            this._container = this.BuildContainer(configurations);
        }

        public async Task ResolveEvent(Event @event)
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

                if (scope.TryResolve(inboxType, out dynamic inbox))
                {
                    await inbox.Execute((dynamic)message, @event);
                    return;
                }

                if (scope.TryResolve(handlerType, out dynamic handler))
                {
                    await handler.Execute((dynamic)message);
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ResolveMessage(Message message)
        {
            using var scope = this._container.BeginLifetimeScope();

            try
            {
                dynamic handler = scope.Resolve(typeof(IHandler<>).MakeGenericType(message.GetType()));
                dynamic dynamicProxy = scope.Resolve(typeof(HandlerDynamicProxy<>).MakeGenericType(message.GetType()));

                await dynamicProxy.Execute(handler, message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IContainer BuildContainer(object configurations)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(HandlerDynamicProxy<>));
            builder = this.BuildHandlerFactories(builder, configurations);

            return builder.Build();
        }

        private ContainerBuilder BuildHandlerFactories(ContainerBuilder builder, object configurations)
        {
            configurations.GetType().Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IHandlerFactory<ContainerBuilder>)) && !t.IsInterface && !t.IsAbstract)
                .Select(m => (IHandlerFactory<ContainerBuilder>)Activator.CreateInstance(m))
                .ToList().ForEach(m => m.Create(builder, configurations));

            return builder;
        }
    }
}