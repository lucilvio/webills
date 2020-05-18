using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal class DependencyResolverWithSimpleInjector
    {
        private readonly Container _container;

        public DependencyResolverWithSimpleInjector()
        {
            this._container = new Container();

            this.RegisterModuleDependencies(this._container);
        }

        internal IBusSubscriber ResolveBusSubscriber()
        {
            return this._container.GetInstance<IBusSubscriber>();
        }

        internal async Task ExecuteComponent<TMessage>(TMessage message)
        {
            var componentType = this.GetComponentTypeByMessage(message);

            if(componentType is null)
                return;

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                dynamic component = this._container.GetInstance(componentType);
                await component.Execute((TMessage)message);
            }
        }

        internal async Task<TOutputMessage> ExecuteComponent<TMessage, TOutputMessage>(TMessage message)
        {
            Type componentType = this.GetComponentTypeByMessage(message);

            if(componentType is null)
                return default;

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                dynamic component = this._container.GetInstance(componentType);
                return await component.Execute((TMessage)message);
            }
        }

        private Type GetComponentTypeByMessage<TMessage>(TMessage message)
        {
            return Assembly.GetExecutingAssembly().GetType(message.GetType().FullName.Replace("Input", "Component"));
        }

        private void RegisterModuleDependencies(Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var inMemoryEventBus = new InMemoryBus();

            container.Register<IBusSender>(() => inMemoryEventBus, Lifestyle.Singleton);
            container.Register<IBusSubscriber>(() => inMemoryEventBus, Lifestyle.Singleton);

            container.Register<TransactionsContext>(Lifestyle.Scoped);
            container.Register<TransactionsReadContext>(Lifestyle.Scoped);

            var currentAssembly = Assembly.GetExecutingAssembly();
            this.RegisterCollection(container, currentAssembly, t => t.Name.EndsWith("DataAccess"), Lifestyle.Scoped);
            this.RegisterCollection(container, currentAssembly, t => t.Name.EndsWith("Component"), Lifestyle.Scoped);

            container.Verify();
        }

        private void RegisterCollection(Container container, Assembly currentAssembly,
            Func<Type, bool> typePredicate, Lifestyle lifestyle)
        {
            currentAssembly.GetTypes()
                .Where(typePredicate)
                .Where(t => !t.IsInterface)
                .Select(t => new { Concrete = t, Interface = t.GetInterfaces().FirstOrDefault() })
                .ToList()
                .ForEach(s => container.Register(s.Interface ?? s.Concrete, s.Concrete, lifestyle));
        }
    }
}