using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;

namespace Lucilvio.Solo.Webills.Transactions
{
    public interface IRemoveIncomeUseCase
    {
        Task Execute(RemoveIncomeCommand command);
    }
}