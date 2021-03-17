using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentExpense
{
    internal class AddNewRecurrentExpenseMessageDispatcher : IMessageDispatcher<AddNewRecurrentExpenseMessage>
    {
        public async Task<dynamic> Dispatch(AddNewRecurrentExpenseMessage message, Configurations configurations = null)
        {
            using var dbConnection = new SqlConnection(configurations.DataConnectionString);

            var dataAccess = new AddNewRecurrentExpenseDataAccess(dbConnection);
            return await new AddNewRecurrentExpenseMessageHandler(dataAccess).Execute(message);
        }
    }
}