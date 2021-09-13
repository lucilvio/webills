using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.DeleteIncome
{
    internal class DeleteIncomeMessageDispatcher : IMessageDispatcher<DeleteIncomeMessage>
    {
        public async Task<dynamic> Dispatch(DeleteIncomeMessage message, Configurations configurations = null)
        {
            using var context = new FinancialControlDataContext(configurations.DataConnectionString);
            using var dbConnection = new SqlConnection(configurations.DataConnectionString);

            var dataAccess = new DeleteIncomeDataAccess(context, dbConnection);
            return await new DeleteIncomeMessageHandler(dataAccess).Execute(message);
        }
    }
}