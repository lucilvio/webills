using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter
{
    public class GetUserTransactionsByFilterMessageDispatcher : IMessageDispatcher<GetUserTransactionsByFilterMessage>
    {
        public async Task<dynamic> Dispatch(GetUserTransactionsByFilterMessage message, Configurations configurations)
        {
            using var context = new FinancialControlDataContext(configurations.DataConnectionString);
            using var dbConnection = new SqlConnection(configurations.DataConnectionString);

            var handler = new GetUserTransactionsByFilterMessageHandler(dbConnection);
            return await handler.Execute(message);
        }
    }
}