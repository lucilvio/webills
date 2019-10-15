using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense
{
    public interface IEditExpense
    {
        Task Execute(EditExpenseCommand command);
    }
}
