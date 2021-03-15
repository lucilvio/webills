using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewIncome
{
    internal class AddNewIncomeMessageDispatcher : IMessageDispatcher<AddNewIncomeMessage>
    {
        public async Task<dynamic> Dispatch(AddNewIncomeMessage message, Configurations configurations)
        {
            using var context = new FinancialControlDataContext(configurations.DataConnectionString);

            var dataAccess = new AddNewIncomeDataAccess(context);
            var handler = new AddNewIncomeMessageHandler(dataAccess);

            return await handler.Execute(message);
        }
    }
}
