using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;

namespace Lucilvio.Solo.Webills.Transactions
{
    public interface IRemoveExpenseUseCase
    {
        Task Execute(RemoveExpenseCommand command);
    }
}