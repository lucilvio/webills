using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.DeleteIncome
{
    internal class DeleteIncomeFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            var configurations = parameters as Configurations;

            container.Register(ctx => new FinancialControlDataContext(configurations)).InstancePerLifetimeScope();
            container.RegisterType<DeleteIncomeDataAccess>().AsSelf().InstancePerLifetimeScope();
            container.RegisterType<DeleteIncome>().As<IHandler<DeleteIncomeMessage>>().InstancePerLifetimeScope();
        }
    }
}