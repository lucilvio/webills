using System.Linq;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Bus;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal class UserCaseResolverBySimpleInjector : IUseCaseResolver
    {
        private readonly Container _container;

        public UserCaseResolverBySimpleInjector(IBus bus)
        {
            this._container = new Container();
            this._container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            this._container.Register<UserAccountContext>(Lifestyle.Scoped);
            this._container.Register<IBus>(() => (Bus.Bus)bus, Lifestyle.Singleton);

            var currentAssembly = typeof(IUseCase<>).Assembly;

            var dataAccessTypes = currentAssembly.GetTypes().Where(t => t.Name.Contains("DataAccess"));
            var c = dataAccessTypes.Where(t => !t.IsInterface);

            foreach (var dataAccessType in dataAccessTypes.Where(t => t.IsInterface))
            {
                var concreteType = c.Where(t => dataAccessType.IsAssignableFrom(t)).FirstOrDefault();

                if (concreteType != null)
                    this._container.Register(dataAccessType, concreteType);
            }

            this._container.Register(typeof(IUseCase<>), currentAssembly);

            this._container.Verify();
        }

        public async Task Resolve(ICommand command)
        {
            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var type = typeof(IUseCase<>).MakeGenericType(command.GetType().BaseType);

                dynamic useCase = this._container.GetInstance(type);
                await useCase.Execute((dynamic)command);
            }
        }
    }
}