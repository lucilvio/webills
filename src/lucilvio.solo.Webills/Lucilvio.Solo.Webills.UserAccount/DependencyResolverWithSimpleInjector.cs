using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal class DependencyResolverWithSimpleInjector
    {
        private readonly Container _container;

        public DependencyResolverWithSimpleInjector()
        {
            this._container = new Container();

            this.ResolveModuleDependencies(this._container);
        }

        internal IBusSubscriber ResolveBusSubscriber()
        {
            return this._container.GetInstance<IBusSubscriber>();
        }

        internal async Task ExecuteComponent<TMessage>(TMessage message)
        {
            var componentType = this.GetComponentTypeByMessage(message);

            if (componentType is null)
                return;

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                dynamic component = this._container.GetInstance(componentType);
                await component.Execute((TMessage)message);
            }
        }

        private Type GetComponentTypeByMessage<TMessage>(TMessage message)
        {
            return Assembly.GetExecutingAssembly().GetType(message.GetType().FullName.Replace("Input", "Component"));
        }

        private void ResolveModuleDependencies(Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var inMemoryEventBus = new InMemoryBus();

            container.Register<IBusSender>(() => inMemoryEventBus, Lifestyle.Singleton);
            container.Register<IBusSubscriber>(() => inMemoryEventBus, Lifestyle.Singleton);

            container.Register<UserAccountContext>(Lifestyle.Scoped);

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