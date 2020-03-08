using System;
using System.Linq;
using System.Reflection;

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

        public Container Container => this._container;

        private void ResolveModuleDependencies(Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            var currentAssembly = Assembly.GetExecutingAssembly();
            
            container.Register<UserAccountContext>(Lifestyle.Scoped);
            this.RegisterCollection(container, currentAssembly, t => t.Name.EndsWith("DataAccess"), Lifestyle.Scoped);
            this.RegisterCollection(container, currentAssembly, t => t.Name.EndsWith("Service"), Lifestyle.Singleton);
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