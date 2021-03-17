using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpenseCategories
{
    internal class GetExpenseCategoriesMessageDispatcher : IMessageDispatcher<GetExpenseCategoriesMessage>
    {
        public async Task<dynamic> Dispatch(GetExpenseCategoriesMessage message, Configurations configurations = null)
        {
            return await new GetExpenseCategoriesMessageHandler().Execute(message);
        }
    }
}