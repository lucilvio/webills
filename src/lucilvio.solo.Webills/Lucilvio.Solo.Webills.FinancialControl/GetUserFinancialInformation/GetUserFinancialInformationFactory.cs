using System.Data;
using Autofac;
using Lucilvio.Solo.Architecture;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserFinancialInformation
{
    public class GetUserFinancialInformationFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object configurations)
        {
            container.Register<IDbConnection>(ctx => new SqlConnection(((Configurations)configurations).DataConnectionString)).InstancePerDependency();
            container.RegisterType<GetUserFinancialInformation>().As<IMessageHandler<GetUserFinancialInformationMessage>>().InstancePerDependency();
        }
    }
}
