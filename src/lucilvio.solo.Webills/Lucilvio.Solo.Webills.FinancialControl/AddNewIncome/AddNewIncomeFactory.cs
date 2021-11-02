using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewIncome
{
    internal class AddNewIncomeFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            container.RegisterType<FinancialControlDataContext>().AsSelf().InstancePerLifetimeScope();
            container.RegisterType<AddNewIncomeDataAccess>().AsSelf().InstancePerLifetimeScope();
            container.RegisterType<AddNewIncome>().As<IHandler<AddNewIncomeMessage>>();
        }
    }
}
