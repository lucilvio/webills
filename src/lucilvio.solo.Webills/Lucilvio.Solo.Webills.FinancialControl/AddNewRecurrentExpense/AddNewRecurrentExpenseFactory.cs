using System.Data;
using Autofac;
using Lucilvio.Solo.Architecture;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentExpense
{
    internal class AddNewRecurrentExpenseFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            var configurations = parameters as Configurations;

            container.Register<IDbConnection>(ctx => new SqlConnection(configurations.DataConnectionString)).InstancePerDependency();
            container.RegisterType<AddNewRecurrentExpenseDataAccess>().AsSelf().InstancePerLifetimeScope();
            container.RegisterDecorator<TransactionScopedHandler<AddNewRecurrentExpenseMessage>, IMessageHandler<AddNewRecurrentExpenseMessage>>();
            container.RegisterType<AddNewRecurrentExpense>().As<IMessageHandler<AddNewRecurrentExpenseMessage>>().InstancePerLifetimeScope();
        }
    }
}