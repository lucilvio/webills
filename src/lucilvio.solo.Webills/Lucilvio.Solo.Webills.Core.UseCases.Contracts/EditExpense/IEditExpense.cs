using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Core.UseCases.Contracts.EditExpense
{
    public interface IEditExpense
    {
        Task Execute(EditExpenseCommand command);
    }
}
