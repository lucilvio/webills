using System.Data;
using Autofac;
using Lucilvio.Solo.Architecture;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter
{
    public class GetUserTransactionsByFilterFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            var configurations = parameters as Configurations;

            container.Register<IDbConnection>(ctx => new SqlConnection(configurations.DataConnectionString)).InstancePerDependency();
            container.RegisterType<GetUserTransactionsByFilter>().As<IMessageHandler<GetUserTransactionsByFilterMessage>>().InstancePerDependency();
        }
    }
}