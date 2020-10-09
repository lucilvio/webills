using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Login;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal class SimpleInjectorMessageResolver : IMessageResolver
    {
        private readonly IEventBus _eventBus;
        private readonly Container _container;

        private readonly IDictionary<Module.Messages, Func<object, Task>> _messagesMap;

        public SimpleInjectorMessageResolver(IEventBus eventBus)
        {
            this._container = ResolveDependencies(new Container());

            this._messagesMap = new Dictionary<Module.Messages, Func<object, Task>>
            {
                { Module.Messages.Login, this.ExecuteLoginComponent }
            };

            this._eventBus = eventBus;
        }

        public async Task Resolve(Module.Messages message, object input)
        {
            if (!this._messagesMap.TryGetValue(message, out var action))
                return;

            await action.Invoke(input);
        }

        internal async Task ExecuteLoginComponent(object input)
        {
            await this._container.GetInstance<LoginComponent>().Execute(input);
        }

        private Container ResolveDependencies(Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register(() => this._eventBus, Lifestyle.Singleton);

            container.Register<UserAccountContext>(Lifestyle.Scoped);

            var currentAssembly = Assembly.GetExecutingAssembly();
            this.RegisterCollection(container, currentAssembly, t => t.Name.EndsWith("DataAccess"), Lifestyle.Scoped);
            this.RegisterCollection(container, currentAssembly, t => t.Name.EndsWith("Component"), Lifestyle.Scoped);

            container.Verify();
            return container;
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