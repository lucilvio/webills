using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomeCategories
{
    internal class GetIncomeCategoriesMessageDispatcher : IMessageDispatcher<GetIncomeCategoriesMessage>
    {
        public async Task<dynamic> Dispatch(GetIncomeCategoriesMessage message, Configurations configurations = null)
        {
            return await new GetIncomeCategoriesMessageHandler().Execute(message);
        }
    }
}