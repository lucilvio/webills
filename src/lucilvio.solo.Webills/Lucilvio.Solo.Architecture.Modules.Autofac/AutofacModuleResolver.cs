using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Lucilvio.Solo.Architecture.EventPublisher.Outbox;
using Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component;
using Lucilvio.Solo.Architecture.Handler.Authorization.Component;
using Lucilvio.Solo.Architecture.Handler.Inbox;
using Lucilvio.Solo.Architecture.Handler.Inbox.Component;
using Lucilvio.Solo.Architecture.Handler.Transaction;
using Newtonsoft.Json;

namespace Lucilvio.Solo.Architecture.Modules.AutofacModule
{
    public class AutofacModuleResolver<TModule> : IModuleResolver<TModule> where TModule : class
    {
        private readonly IContainer _container;

        public AutofacModuleResolver(Action<ContainerBuilder> registerGeneralDependencies = null)
        {
            var moduleAssembly = typeof(TModule).Assembly;
            this._container = BuildContainer(registerGeneralDependencies, moduleAssembly);
        }

        public async Task Resolve(object objectToResolve)
        {
            if (objectToResolve is null)
                return;

            if (objectToResolve is Message)
                await this.ResolveMessage(objectToResolve as Message);
            else if (objectToResolve is Event)
                await this.ResolveEvent(objectToResolve as Event);
        }

        private async Task ResolveEvent(Event @event)
        {
            var messageType = Array.Find(typeof(TModule).Assembly.GetTypes(),
                t => t.Name.Equals($"{@event.Name}Message", StringComparison.InvariantCultureIgnoreCase));

            if (messageType is null)
                return;

            var messageHandler = typeof(IMessageHandler<>).MakeGenericType(messageType);

            if (messageHandler is null)
                return;

            using var scope = this._container.BeginLifetimeScope(c =>
            {
                if (messageType.GetCustomAttribute<InboxAttribute>(false) is not null)
                {
                    c.RegisterInbox(messageType);
                    c.RegisterGeneric(typeof(InboxDynamicProxy<>)).SingleInstance();
                }
            });

            if (scope.TryResolve(typeof(IInbox<>).MakeGenericType(messageType), out dynamic inboxHandler))
            {
                var message = DesserializeMessage(@event.Payload, messageType);
                dynamic dynamicProxy = scope.Resolve(typeof(InboxDynamicProxy<>).MakeGenericType(messageType));
                await dynamicProxy.Execute(inboxHandler, (dynamic)message, @event);
            }
            else if (scope.TryResolve(messageHandler, out dynamic handler))
            {
                var message = DesserializeMessage(@event.Payload, messageType);
                dynamic dynamicProxy = scope.Resolve(typeof(HandlerDynamicProxy<>).MakeGenericType(messageType));
                await dynamicProxy.Execute(handler, message);
            }
            else
            {
                throw new Error($"No handler found for the message {messageType}");
            }
        }

        private async Task ResolveMessage(Message message)
        {
            var messageType = message.GetType();

            var messageHandler = typeof(IMessageHandler<>).MakeGenericType(messageType);

            using var scope = this._container.BeginLifetimeScope(cfg =>
            {
                if (messageType.GetCustomAttribute<TransactionAttribute>(false) is not null)
                {
                    var g = typeof(TransactionScopedHandler<>).MakeGenericType(messageType);
                    cfg.RegisterDecorator(g, messageHandler);
                }

                if (messageType.GetCustomAttribute<OutboxAttribute>(false) is not null)
                {
                    var g = typeof(TransactionScopedHandler<>).MakeGenericType(messageType);

                    if (messageType.GetCustomAttribute<TransactionAttribute>(false) is null)
                        cfg.RegisterDecorator(g, messageHandler);

                    cfg.RegisterOutbox();
                }

                if (messageType.GetCustomAttribute<AllowedRolesAttribute>(false) is not null)
                {
                    var g = typeof(AuthorizationInterceptor<>).MakeGenericType(messageType);
                    cfg.RegisterDecorator(g, messageHandler);
                }
            });

            if (!scope.TryResolve(messageHandler, out dynamic handler))
                throw new Error($"No handler found for the message {messageType}");

            dynamic dynamicProxy = scope.Resolve(typeof(HandlerDynamicProxy<>).MakeGenericType(messageType));
            await dynamicProxy.Execute(handler, message);
        }

        private static IContainer BuildContainer(Action<ContainerBuilder> registerGeneralDependencies,
            Assembly moduleAssembly)
        {
            var builder = new ContainerBuilder();

            if (registerGeneralDependencies is not null)
                registerGeneralDependencies(builder);

            builder.RegisterGeneric(typeof(HandlerDynamicProxy<>)).SingleInstance();

            builder = BuildMessageHandlers(builder, moduleAssembly);
            builder = BuildDataAccesses(builder, moduleAssembly);

            return builder.Build();
        }

        private static ContainerBuilder BuildMessageHandlers(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IMessageHandler<>))
                .InstancePerLifetimeScope();

            return builder;
        }

        private static ContainerBuilder BuildDataAccesses(ContainerBuilder builder, Assembly moduleAssembly)
        {
            builder.RegisterAssemblyTypes(moduleAssembly, typeof(OutboxAttribute).Assembly)
                .Where(t => t.Name.EndsWith("DataAccess"))
                .AsSelf()
                .InstancePerLifetimeScope();

            return builder;
        }

        private static object DesserializeMessage(object payload, Type messageType) =>
            JsonConvert.DeserializeObject(JsonConvert.SerializeObject(payload), messageType);
    }
}