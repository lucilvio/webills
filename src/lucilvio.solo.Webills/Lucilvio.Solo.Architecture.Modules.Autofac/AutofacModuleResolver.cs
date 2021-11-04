using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Lucilvio.Solo.Architecture.Handler.Inbox;
using Newtonsoft.Json;

namespace Lucilvio.Solo.Architecture.Modules.AutofacModule
{
    public class AutofacModuleResolver<TModule> : IModuleResolver<TModule> where TModule : class
    {
        private readonly IContainer _container;
        private readonly Type[] _interceptors;

        public AutofacModuleResolver(object parameters, params Type[] interceptors)
        {
            this._container = this.BuildContainer(parameters);
            this._interceptors = interceptors;
        }

        public async Task Resolve(object objectToResolve)
        {
            if (objectToResolve is null)
                return;

            if (objectToResolve is Message)
            {
                await this.ResolveMessage(objectToResolve as Message);
                return;
            }
            else if (objectToResolve is Event)
            {
                await this.ResolveEvent(objectToResolve as Event);
                return;
            }
            else
                return;
        }

        private async Task ResolveEvent(Event @event)
        {
            var messageType = this.GetType().Assembly.GetTypes()
                .FirstOrDefault(t => t.Name.Equals($"{@event.Name}Message", StringComparison.InvariantCultureIgnoreCase));

            if (messageType is null)
                return;

            var handlerType = typeof(IMessageHandler<>).MakeGenericType(messageType);
            var inboxType = typeof(Inbox<>).MakeGenericType(messageType);

            if (messageType is null || handlerType is null)
                return;

            using var scope = this._container.BeginLifetimeScope();

            if (scope.TryResolve(inboxType, out dynamic inbox))
            {
                var message = DesserializeMessage(@event.Payload, messageType);
                await inbox.Execute((dynamic)message, @event);
                return;
            }

            if (scope.TryResolve(handlerType, out dynamic handler))
            {
                var message = DesserializeMessage(@event.Payload, messageType);
                await handler.Execute((dynamic)message);
                return;
            }
        }

        private async Task ResolveMessage(Message message)
        {
            var messageType = message.GetType();
            var handlerType = typeof(IMessageHandler<>).MakeGenericType(messageType);

            using var scope = this._container.BeginLifetimeScope(cfg =>
            {
                foreach (var interceptor in this._interceptors)
                {
                    var interceptorType = interceptor.MakeGenericType(messageType);
                    cfg.RegisterDecorator(interceptorType, handlerType);
                }
            });

            if (!scope.TryResolve(handlerType, out dynamic handler))
                throw new Error($"No handler found for the message {messageType}");

            dynamic dynamicProxy = scope.Resolve(typeof(HandlerDynamicProxy<>).MakeGenericType(messageType));
            await dynamicProxy.Execute(handler, message);
        }

        private IContainer BuildContainer(object parameters)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(HandlerDynamicProxy<>));
            builder = this.BuildHandlerFactories(builder, parameters);

            return builder.Build();
        }

        private ContainerBuilder BuildHandlerFactories(ContainerBuilder builder, object parameters)
        {
            typeof(TModule).Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IHandlerFactory<ContainerBuilder>)) && !t.IsInterface && !t.IsAbstract)
                .Select(m => (IHandlerFactory<ContainerBuilder>)Activator.CreateInstance(m))
                .ToList().ForEach(m => m.Create(builder, parameters));

            return builder;
        }

        private static object DesserializeMessage(object payload, Type messageType) =>
            JsonConvert.DeserializeObject(JsonConvert.SerializeObject(payload), messageType);
    }
}