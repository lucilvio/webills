using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewExpense
{
    internal class AddNewExpenseMessageDispatcher : IMessageDispatcher<AddNewExpenseMessage>
    {
        public async Task<dynamic> Dispatch(AddNewExpenseMessage message, Configurations configurations)
        {
            using var context = new FinancialControlDataContext(configurations.DataConnectionString);

            var dataAccess = new AddNewExpenseDataAccess(context);
            var handler = new AddNewExpenseMessageHandler(dataAccess);

            return await handler.Execute(message);
        }
    }
}
