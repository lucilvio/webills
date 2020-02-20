using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Transactions.EditIncome;

namespace Lucilvio.Solo.Webills.Transactions
{
    public interface IEditExpenseUseCase
    {
        Task Execute(EditExpenseCommand command);
    }
}