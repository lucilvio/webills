using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    public interface IAddNewExpenseUseCase
    {
        Task Execute(AddNewExpenseCommand command);
    }
}