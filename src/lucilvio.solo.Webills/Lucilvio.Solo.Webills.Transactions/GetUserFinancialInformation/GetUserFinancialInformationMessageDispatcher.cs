using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserFinancialInformation
{
    public class GetUserFinancialInformationMessageDispatcher : IMessageDispatcher<GetUserFinancialInformationMessage>
    {
        public async Task<dynamic> Dispatch(GetUserFinancialInformationMessage message, Configurations configurations)
        {
            using var context = new FinancialControlDataContext(configurations.DataConnectionString);
            using var dbConnection = new SqlConnection(configurations.DataConnectionString);

            var handler = new GetUserFinancialInformationMessageHandler(dbConnection);
            return await handler.Execute(message);
        }
    }
}
