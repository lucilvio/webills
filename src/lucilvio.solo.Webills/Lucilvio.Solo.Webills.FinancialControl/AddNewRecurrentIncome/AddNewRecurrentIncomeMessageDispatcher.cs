using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentIncome
{
    internal class AddNewRecurrentIncomeMessageDispatcher : IMessageDispatcher<AddNewRecurrentIncomeMessage>
    {
        public async Task<dynamic> Dispatch(AddNewRecurrentIncomeMessage message, Configurations configurations = null)
        {
            using var dbConnection = new SqlConnection(configurations.DataConnectionString);

            var dataAccess = new AddNewRecurrentIncomeDataAccess(dbConnection);
            return await new AddNewRecurrentIncomeMessageHandler(dataAccess).Execute(message);
        }
    }
}