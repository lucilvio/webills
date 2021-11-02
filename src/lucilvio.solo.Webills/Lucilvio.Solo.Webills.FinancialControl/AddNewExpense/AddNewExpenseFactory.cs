using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewExpense
{
    internal class AddNewExpenseFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            var configurations = parameters as Configurations;

            container.Register(ctx => new FinancialControlDataContext(configurations)).InstancePerLifetimeScope();

            container.RegisterType<AddNewExpenseDataAccess>().AsSelf().InstancePerLifetimeScope();
            container.RegisterDecorator<TransactionScopedHandler<AddNewExpenseMessage>, IHandler<AddNewExpenseMessage>>();
            container.RegisterType<AddNewExpense>().As<IHandler<AddNewExpenseMessage>>().InstancePerLifetimeScope();
        }
    }
}
