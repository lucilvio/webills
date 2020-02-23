using System.Linq;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal class UseCaseResolverBySimpleInjector : IUseCaseResolver
    {
        private readonly Container _container;

        public UseCaseResolverBySimpleInjector()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            _container.Register<TransactionsContext>(Lifestyle.Scoped);

            var currentAssembly = typeof(IUseCase<>).Assembly;

            var dataAccessTypes = currentAssembly.GetTypes().Where(t => t.Name.Contains("DataAccess"));
            var c = dataAccessTypes.Where(t => !t.IsInterface);

            foreach (var dataAccessType in dataAccessTypes.Where(t => t.IsInterface))
            {
                var concreteType = c.Where(t => dataAccessType.IsAssignableFrom(t)).FirstOrDefault();

                if (concreteType != null)
                    _container.Register(dataAccessType, concreteType);
            }

            _container.Register(typeof(IUseCase<>), currentAssembly);

            _container.Verify();
        }

        public async Task Resolve(ICommand command)
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var type = typeof(IUseCase<>).MakeGenericType(command.GetType().BaseType);

                dynamic useCase = _container.GetInstance(type);
                await useCase.Execute((dynamic)command);
            }
        }
    }
}